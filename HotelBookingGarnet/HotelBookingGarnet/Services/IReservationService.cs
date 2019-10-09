using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBookingGarnet.Models;

namespace HotelBookingGarnet.Services
{
    public interface IReservationService
    {
        Task<List<Reservation>> FindReservationByHotelIdAsync(long hotelId);
    }
}