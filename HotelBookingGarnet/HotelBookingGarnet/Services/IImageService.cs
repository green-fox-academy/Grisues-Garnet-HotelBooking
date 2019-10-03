using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace HotelBookingGarnet.Services
{
    public interface IImageService
    {
        Task UploadAsync(IFormFileCollection files);
        Task<IEnumerable<Uri>> ListAsync();
        Task<CloudBlobDirectory> GetFolder(long id);
    }
}
