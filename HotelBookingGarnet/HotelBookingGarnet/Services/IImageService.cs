using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;
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
        Task<List<IListBlobItem>> ListDirectoryAsync();
        Task<List<ImageDetails>> ListAsync(long id);
        Task<List<ImageDetails>> ListAllFoldersAsync();
        List<string> Validate(IFormFileCollection files, HotelViewModel newHotel);
     }
}
