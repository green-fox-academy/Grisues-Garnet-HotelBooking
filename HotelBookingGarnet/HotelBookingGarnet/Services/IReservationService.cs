using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;


namespace HotelBookingGarnet.Services
{
    public interface IReservationService
    {

        Task<List<Reservation>> FindReservationsByHotelIdAsync(long hotelId);
        Task<List<Reservation>> FindReservationByUserIdAsync(string userId);
        Task DeleteReservationByIdAsync(long reservationId);
        Task DeleteExpiredReservationByIdAsync(string userId);
        Task<long> AddReservationAsync(ReservationViewModel newReservation, string userId, long roomId, long hotelId);
        Task<Reservation> FindReservationByUserIdAsync(long reservationId);
    }
}