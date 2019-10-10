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

        public async Task DeleteReservationById(long reservationId)
        {
            var reservation =
                await applicationContext.Reservations.FirstOrDefaultAsync(r => r.ReservationId == reservationId);
            applicationContext.Reservations.Remove(reservation);
            applicationContext.SaveChanges();
        }

        public async Task<List<Reservation>> FindReservationsByUserIdAsync(string userId)
        {
            var reservations = await applicationContext.Reservations.Include(r => r.GuestsList)
                .Where(a => a.UserId == userId).OrderBy(a => a.ReservationStart).ToListAsync();

            return reservations;
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

        public async Task<Reservation> FindReservationByIdAsync(long reservationId)
        {
            var foundReservation = await applicationContext.Reservations.Include(p => p.GuestsList)
                .FirstOrDefaultAsync(a => a.ReservationId == reservationId);
            return foundReservation;
        }
    }
}