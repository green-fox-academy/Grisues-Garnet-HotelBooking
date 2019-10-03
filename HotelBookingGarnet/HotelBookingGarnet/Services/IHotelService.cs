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
        Task EditHotelAsync(long HotelId, HotelViewModel editHotel);
        Task<Hotel> FindHotelByIdAsync(long HotelId);
        Task AddHotelAsync(HotelViewModel newHotel, string userId);
        Task<PagingList<Hotel>> FilterHotelsAsync(QueryParam queryParam);
    }
}