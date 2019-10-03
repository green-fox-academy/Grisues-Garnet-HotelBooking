using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

namespace HotelBookingGarnet.Services
{
    public class BlobService : IBlobService
    {
        private CloudBlobClient blobClient;
        private CloudBlobContainer blobContainer;

        public async Task<CloudBlobContainer> GetBlobContainer()
        {
            var containerName = "photos";
            if (string.IsNullOrWhiteSpace(containerName))
            {
                throw new ArgumentException("Configuration must contain ContainerName");
            }

            var blobClient = GetClient();
            blobContainer = blobClient.GetContainerReference(containerName);
            if (await blobContainer.CreateIfNotExistsAsync())
            {
                await blobContainer.SetPermissionsAsync(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });
            }
            return blobContainer;
        }

        private CloudBlobClient GetClient()
        {
            if (blobClient != null)
                return blobClient;

            var storageConnectionString = "DefaultEndpointsProtocol=https;AccountName=testphotos;AccountKey=XmgkCr7+8migTd1u8Y9rO1Uag9fGyLKkcwgoTcBv6jNtD/OnDGYclnlSI6cXfg5E75i3u2xkpanv6XsEMb2agA==;EndpointSuffix=core.windows.net";
            if (string.IsNullOrWhiteSpace(storageConnectionString))
            {
                throw new ArgumentException("Configuration must contain StorageConnectionString");
            }

            if (!CloudStorageAccount.TryParse(storageConnectionString, out CloudStorageAccount storageAccount))
            {
                throw new Exception("Could not create storage account with StorageConnectionString configuration");
            }

            blobClient = storageAccount.CreateCloudBlobClient();
            return blobClient;
        }
    }
}
