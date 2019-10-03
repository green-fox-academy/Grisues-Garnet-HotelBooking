using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;

namespace HotelBookingGarnet.Services
{
    public interface IHotelService

    {
       //Task<List<Hotel>> FindAllHotelAsync();
        List<Hotel> GetHotels();
        Task EditHotelAsync(long HotelId, HotelViewModel editHotel);
        Task<Hotel> FindHotelByIdAsync(long HotelId);
        Task<long> AddHotelAsync(HotelViewModel newHotel, string userId);
        Task<Hotel> FindHotelByName(string hotelName);
    }
}
