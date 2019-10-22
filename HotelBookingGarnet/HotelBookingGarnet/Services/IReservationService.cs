using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;
using System;

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
        Task <List<string>> ReservationValidationAsync(ReservationViewModel newReservation, long roomId);
        Task <List<Room>> FindAvailableRoomByHotelIdAndDateAsync(long hotelId,
                                                                 DateTime reservationStart,
                                                                 DateTime reservationEnd);
    }
}