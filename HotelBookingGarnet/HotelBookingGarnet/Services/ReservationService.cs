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

        public ReservationService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task<List<Reservation>> FindReservationsByHotelIdAsync(long hotelId)
        {
            var hotelReservations = await applicationContext.Reservations.Where(r => r.HotelId == hotelId).ToListAsync();

            return hotelReservations;
        }

        public async Task DeleteReservationById(long reservationId)
        {
            var reservation =
                await applicationContext.Reservations.FirstOrDefaultAsync(r => r.ReservationId == reservationId);
            applicationContext.Reservations.Remove(reservation);
            applicationContext.SaveChanges();
        }

        public async Task<List<Reservation>> FindReservationsByIdAsync(string userId)
        {
            var reservations = await applicationContext.Reservations.Where(a => a.UserId == userId).ToListAsync();

            return reservations;
        }
    }
}