using HotelBookingGarnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingGarnet.Services
{
    public interface IHotelService
    {
       Task<List<Hotel>> findAllHotelAsync();
        Task<Hotel> findHotelByIdAsync(long id);
        List<Hotel> GetHotels();
    }
}
