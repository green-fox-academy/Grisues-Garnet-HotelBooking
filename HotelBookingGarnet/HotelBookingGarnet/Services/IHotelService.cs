using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.Utils;
using HotelBookingGarnet.ViewModels;
using ReflectionIT.Mvc.Paging;

namespace HotelBookingGarnet.Services
{
    public interface IHotelService
    {
        List<Hotel> GetHotels();
        Task EditHotelAsync(long hotelId, HotelViewModel editHotel);
        Task<Hotel> FindHotelByIdAsync(long hotelId);
        Task<long> AddHotelAsync(HotelViewModel newHotel, string userId);   
        Task<Hotel> FindHotelByName(string hotelName);
        Task<PagingList<Hotel>> FilterHotelsAsync(QueryParam queryParam);
    }
}