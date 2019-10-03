using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var stringID = id.ToString();
            var folder = blobcontainer.GetDirectoryReference(stringID);
            return folder;
        }

        public async Task<IEnumerable<Uri>> ListAsync(long id)
        {
            var blobContainer = await blobService.GetBlobContainer();
            var allBlobs = new List<Uri>();
            var stringID = id.ToString();
            var response = blobContainer.ListBlobs(stringID, useFlatBlobListing: true);
            foreach (IListBlobItem blob in response)
            {
              if (blob.GetType() == typeof(CloudBlockBlob))
                        allBlobs.Add(blob.Uri);
            }
            return allBlobs;
        }

        public async Task<List<IListBlobItem>> ListDirectoryAsync()
        {
            var blobContainer = await blobService.GetBlobContainer();
            CloudBlobDirectory dir = blobContainer.GetDirectoryReference("photos");
            bool useFlatBlobListing = false;
            var blobs = blobContainer.ListBlobs(null, useFlatBlobListing, BlobListingDetails.None);
            var folders = blobs.Where(b => b as CloudBlobDirectory != null).ToList();
            return folders;
        }

        public async Task UploadAsync(IFormFileCollection files,long id)
        {
            var blobcontainer = await blobService.GetBlobContainer();
            //var folder = blobcontainer.GetDirectoryReference("1");
            //var folder = blobcontainer.GetDirectoryReference(i);
            for (int i = 0; i < files.Count; i++)
            {
                var blob = blobcontainer.GetBlockBlobReference(id+"/"+files[i].FileName);
                
                using (var stream = files[i].OpenReadStream())
                {
                    await blob.UploadFromStreamAsync(stream);
                }
            }
        }  
    }
}
