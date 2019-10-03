using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HotelBookingGarnet.Services
{
    public interface IImageService
    {
        Task UploadAsync(IFormFileCollection files, long Id);
        Task<IEnumerable<Uri>> ListAsync(long id);
        Task<List<IListBlobItem>> ListDirectoryAsync();
        Task<CloudBlobDirectory> GetFolder(long id);
    }
}
