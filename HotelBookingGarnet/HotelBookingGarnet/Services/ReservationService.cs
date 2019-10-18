using HotelBookingGarnet.ViewModels;
using AutoMapper;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingGarnet.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ApplicationContext applicationContext;
        private readonly IMapper mapper;
        private readonly IGuestService guestService;
        private readonly IRoomService roomService;

        public ReservationService(IMapper mapper, ApplicationContext applicationContext, IGuestService guestService,
            IRoomService roomService)
        {
            this.mapper = mapper;
            this.applicationContext = applicationContext;
            this.guestService = guestService;
            this.roomService = roomService;
        }

        public async Task<List<Reservation>> FindReservationsByHotelIdAsync(long hotelId)
        {
            var hotelReservations = await applicationContext.Reservations.Include(r => r.GuestsList)
                .Where(r => r.HotelId == hotelId).OrderBy(a => a.ReservationStart).ToListAsync();
            return hotelReservations;
        }

        public async Task<List<Reservation>> FindReservationsByRoomIdAsync(long roomId)
        {
            var hotelReservations = await applicationContext.Reservations.Include(r => r.GuestsList)
                .Where(r => r.RoomId == roomId).OrderBy(a => a.ReservationStart).ToListAsync();
            return hotelReservations;
        }

        public async Task<List<Reservation>> FindReservationByReservationIdAsync(string userId)
        {
            var reservations = await applicationContext.Reservations.Include(r => r.GuestsList)
                .Where(a => a.UserId == userId).OrderBy(a => a.ReservationStart).ToListAsync();

            return reservations;
        }

        public async Task<Reservation> FindReservationByReservationIdAsync(long reservationId)
        {
            var foundReservation = await applicationContext.Reservations.Include(p => p.GuestsList)
                .FirstOrDefaultAsync(a => a.ReservationId == reservationId);
            return foundReservation;
        }

        public async Task DeleteExpiredReservationByIdAsync(string userId)
        {
            var expiredReservation = await applicationContext.Reservations
                .Where(r => DateTime.Now.AddDays(-30) >= r.ReservationEnd && r.UserId == userId).ToListAsync();
            foreach (var reservation in expiredReservation)
            {
                applicationContext.Reservations.Remove(reservation);
            }

            applicationContext.SaveChanges();
        }

        public async Task DeleteReservationByIdAsync(long reservationId)
        {
            var reservation =
                await applicationContext.Reservations.FirstOrDefaultAsync(r => r.ReservationId == reservationId);
            applicationContext.Reservations.Remove(reservation);
            applicationContext.SaveChanges();
        }

        public async Task<long> AddReservationAsync(ReservationViewModel newReservation, string userId, long roomId,
            long hotelId)
        {
            var reservation = mapper.Map<ReservationViewModel, Reservation>(newReservation);
            reservation.UserId = userId;
            reservation.RoomId = roomId;
            reservation.HotelId = hotelId;
            reservation.TotalPrice = await CalculatePriceAsync(roomId, reservation);
            await applicationContext.Reservations.AddAsync(reservation);
            await applicationContext.SaveChangesAsync();
            await guestService.SaveGuestAsync(newReservation, reservation.ReservationId);
            return reservation.ReservationId;
        }

        private async Task<int> CalculatePriceAsync(long roomId, Reservation reservation)
        {
            var room = await roomService.FindRoomByIdAsync(roomId);
            var daysOfReservation = (int) (reservation.ReservationEnd - reservation.ReservationStart).TotalDays + 1;
            return room.Price * daysOfReservation;
        }

        public List<string> ReservationValidation(ReservationViewModel newReservation, long roomId)
        {
            var dateValid = DateValidation(newReservation);
            var occupationValid = OccupationValidation(newReservation, roomId);
            var guestValid = GuestNumberValidation(newReservation, roomId);

            if (dateValid != null)
                newReservation.ErrorMessages.Add(dateValid);
            if (occupationValid != null)
                newReservation.ErrorMessages.Add(occupationValid);
            if (guestValid != null)
                newReservation.ErrorMessages.Add(guestValid);

            return newReservation.ErrorMessages;
        }

        private static string GuestNumberValidation(ReservationViewModel newReservation, long roomId)
        {
            var guestNameListSize = newReservation.GuestsNameInString.Split(", ").Length;
            return newReservation.NumberOfGuest != guestNameListSize
                ? "The specified guests does not match with the given guest number!"
                : null;
        }

        private string OccupationValidation(ReservationViewModel newReservation, long roomId)
        {
            var startDate = newReservation.ReservationStart;
            var endDate = newReservation.ReservationEnd;

            foreach (var reservation in FindReservationsByRoomIdAsync(roomId).Result)
            {
                var occupationStart = reservation.ReservationStart;
                var occupationEnd = reservation.ReservationEnd;

                if (occupationStart <= startDate && startDate <= occupationEnd ||
                    occupationStart <= endDate && endDate <= occupationEnd ||
                    startDate <= occupationStart && occupationEnd <= endDate)
                {
                    var styledStartDate = reservation.ReservationStart.ToString("yyyy/MM/dd");
                    var styledEndDate = reservation.ReservationEnd.ToString("yyyy/MM/dd");
                    return $"Room is already booked in this interval:  {styledStartDate} - {styledEndDate}";
                }
            }

            return null;
        }

        private static string DateValidation(ReservationViewModel newReservation)
        {
            var startDate = newReservation.ReservationStart;
            var endDate = newReservation.ReservationEnd;

            if (startDate > endDate)
            {
                return "The end of the booking must not precede the start time!";
            }
            return startDate < DateTime.Today ? "The booking cannot begin earlier than today!" : null;
        }
    }
}