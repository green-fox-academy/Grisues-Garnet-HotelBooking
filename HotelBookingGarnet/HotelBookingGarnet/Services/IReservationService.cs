using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBookingGarnet.Models;

namespace HotelBookingGarnet.Services
{
    public interface IReservationService
    {
        Task<List<Reservation>> FindReservationByHotelIdAsync(long hotelId);
        Task<List<Reservation>> FindReservationByIdAsync(string userId);
        Task DeleteReservationById(long reservationId);
    }
}