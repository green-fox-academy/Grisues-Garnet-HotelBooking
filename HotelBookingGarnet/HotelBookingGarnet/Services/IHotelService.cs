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
        List<Hotel> GetHotels();
        Task EditHotelAsync(long hotelId, HotelViewModel editHotel);
        Task<Hotel> FindHotelByIdAsync(long hotelId);
        Task AddHotelAsync(HotelViewModel newHotel, string userId);
    }
}