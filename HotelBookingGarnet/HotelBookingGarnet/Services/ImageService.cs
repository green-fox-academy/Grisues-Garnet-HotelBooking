using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;
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
        private int fourMegaByte = 4 * 1024 * 1024;
        private readonly string[] validExtensions = { "jpg", "png" };

        public ImageService(IBlobService blobService)
        {
            this.blobService = blobService;
        }

        public async Task<List<ImageDetails>> ListAsync(long id)
        {
            var imageList = new List<ImageDetails>();
            BlobContinuationToken blobContinuationToken = null;
            do
            {
               var blobContainer = await blobService.GetBlobContainer();
               var response = await blobContainer.ListBlobsSegmentedAsync(null, blobContinuationToken);
                blobContinuationToken = response.ContinuationToken;
               await GetBlobDirectory(imageList, id);
            } while (blobContinuationToken != null);
            return imageList;
        }

        private async Task GetBlobDirectory(List<ImageDetails> imageList, long id)
        {
            var blobContainer = await blobService.GetBlobContainer();
            foreach (var item in blobContainer.ListBlobs())
            {
                if (item is CloudBlobDirectory)
                {
                    GetImagesFromBlobs(item, imageList, id);
                }
            }
        }

        private void GetImagesFromBlobs(IListBlobItem item, List<ImageDetails> imageList, long id)
        {
            CloudBlobDirectory directory = (CloudBlobDirectory)item;
            IEnumerable<IListBlobItem> blobs = directory.ListBlobs(true);
            foreach (var blob in blobs)
            {
                var folderId = GetFolders(blob.Uri);
                if (id == folderId)
                {
                    imageList.Add(new ImageDetails
                    {
                        Name = blob.Uri.Segments[blob.Uri.Segments.Length-1],
                        Path = blob.Uri.ToString()  
                    });
                }
            }
        }

        private static int GetFolders(Uri uri)
        {
            var path = uri.ToString().Split("/");
            var folder = path[path.Length - 2];
            return Convert.ToInt32(folder);
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

        public List<string> Validate(IFormFileCollection files, HotelViewModel newHotel)
        {
            for (int i = 0; i < files.Count; i++)
            {
                if (CheckImageExtension(files[i]))
                {
                    if (files[i].Length < fourMegaByte)
                    {
                    }
                    else
                    {
                        newHotel.ErrorMessages.Add("The image max 4 MB");
                        return newHotel.ErrorMessages;
                    }
                }
                else
                {
                newHotel.ErrorMessages.Add("Please add only image formats!");
                return newHotel.ErrorMessages;
            }
        }
            return newHotel.ErrorMessages;
        }

        public async Task UploadAsync(IFormFileCollection files,long id)
        {
            var blobcontainer = await blobService.GetBlobContainer();
            for (int i = 0; i < files.Count; i++)
            {
                var blob = blobcontainer.GetBlockBlobReference(id + "/" + files[i].FileName);
                using (var stream = files[i].OpenReadStream())
                {
                    await blob.UploadFromStreamAsync(stream);
                }
            }
        }

        private bool CheckImageExtension(IFormFile file)
        {
            var fileNameSegments = file.FileName.Split(".");
            var extensions = new List<string>(validExtensions);
            return extensions.Contains(fileNameSegments[fileNameSegments.Length-1]);
        }

        public async Task<List<ImageDetails>> ListAllFoldersAsync()
        {
            var imageList = new List<ImageDetails>();
            BlobContinuationToken blobContinuationToken = null;
            do
            {
                var blobContainer = await blobService.GetBlobContainer();
                var response = await blobContainer.ListBlobsSegmentedAsync(null, blobContinuationToken);
                blobContinuationToken = response.ContinuationToken;
                await GetAllBlobDirectory(imageList);
            } while (blobContinuationToken != null);
            return imageList;
        }

        private async Task GetAllBlobDirectory(List<ImageDetails> imageList)
        {
            var blobContainer = await blobService.GetBlobContainer();
            foreach (var item in blobContainer.ListBlobs())
            {
                if (item is CloudBlobDirectory)
                {
                    GetAllImagesFromBlobs(item, imageList);
                }
            }
        }

        private void GetAllImagesFromBlobs(IListBlobItem item, List<ImageDetails> imageList)
        {
            CloudBlobDirectory directory = (CloudBlobDirectory)item;
            IEnumerable<IListBlobItem> blobs = directory.ListBlobs();
            var blob = blobs.First();
            imageList.Add(new ImageDetails
            {
                Name = blob.Uri.Segments[blob.Uri.Segments.Length - 1],
                Path = blob.Uri.ToString()
            });
        }
    }
}
