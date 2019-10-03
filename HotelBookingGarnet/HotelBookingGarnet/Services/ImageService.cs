using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HotelBookingGarnet.Services
{
    public class ImageService:IImageService
    {
        private readonly IBlobService blobService;

        public ImageService(IBlobService blobService)
        {
            this.blobService = blobService;
        }

        public async Task<CloudBlobDirectory> GetFolder(long id)
        {
            var blobcontainer = await blobService.GetBlobContainer();
            var folder = blobcontainer.GetDirectoryReference("1");
            return folder;
        }

        public async Task<IEnumerable<Uri>> ListAsync()
        {
            var blobContainer = await blobService.GetBlobContainer();
            var allBlobs = new List<Uri>();
            BlobContinuationToken blobContinuationToken = null;
            do
            {
                var response = await blobContainer.ListBlobsSegmentedAsync(blobContinuationToken);
                foreach (IListBlobItem blob in response.Results)
                {
                    if (blob.GetType() == typeof(CloudBlockBlob))
                        allBlobs.Add(blob.Uri);
                }
                blobContinuationToken = response.ContinuationToken;
            } while (blobContinuationToken != null);
            return allBlobs;
        }

        public async Task UploadAsync(IFormFileCollection files)
        {
            var blobcontainer = await blobService.GetBlobContainer();
            var folder = blobcontainer.GetDirectoryReference("1");
            for (int i = 0; i < files.Count; i++)
            {

                var blob = folder.GetBlockBlobReference(files[i].FileName);
                
                using (var stream = files[i].OpenReadStream())
                {
                    await blob.UploadFromStreamAsync(stream);
                }
            }
        }  
    }
}
