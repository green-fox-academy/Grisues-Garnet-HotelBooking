using HotelBookingGarnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingGarnet.Services
{
    public interface IHotelService
    {
       Task<List<Hotel>> FindAllHotelAsync();
        Task<Hotel> FindHotelByIdAsync(long id);
        List<Hotel> GetHotels();
    }
}
