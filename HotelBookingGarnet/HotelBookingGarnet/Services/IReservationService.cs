using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;


namespace HotelBookingGarnet.Services
{
    public interface IReservationService
    {
        Task<List<Reservation>> FindReservationByHotelIdAsync(long hotelId);
        Task<List<Reservation>> FindReservationByIdAsync(string userId);
        Task DeleteReservationById(long reservationId);
        Task<long> AddReservationAsync(ReservationViewModel newReservation, string userId, long roomId, long hotelId);
        Task<Reservation> FindReservationByIdAsync(long reservationId);
    }
}