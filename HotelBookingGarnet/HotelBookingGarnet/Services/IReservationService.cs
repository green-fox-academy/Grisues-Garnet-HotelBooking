using System.Threading.Tasks;
using System.Collections.Generic;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;


namespace HotelBookingGarnet.Services
{
    public interface IReservationService
    {

        Task<List<Reservation>> FindReservationsByHotelIdAsync(long hotelId);
        Task<List<Reservation>> FindReservationsByRoomIdAsync(long roomId);
        Task<List<Reservation>> FindReservationByReservationIdAsync(string userId);
        Task DeleteReservationByIdAsync(long reservationId);
        Task DeleteExpiredReservationByIdAsync(string userId);
        Task<long> AddReservationAsync(ReservationViewModel newReservation, string userId, long roomId, long hotelId);
        Task<Reservation> FindReservationByReservationIdAsync(long reservationId);
        List<string> ReservationValidation(ReservationViewModel newReservation, long roomId);
    }
}