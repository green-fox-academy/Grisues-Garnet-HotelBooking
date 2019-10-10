using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBookingGarnet.Models;

namespace HotelBookingGarnet.Services
{
    public interface IReservationService
    {
        Task<List<Reservation>> FindReservationsByHotelIdAsync(long hotelId);
        Task<List<Reservation>> FindReservationsByIdAsync(string userId);
        Task DeleteReservationById(long reservationId);
    }
}