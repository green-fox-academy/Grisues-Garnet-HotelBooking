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

        public async Task<List<Reservation>> FindReservationByHotelIdAsync(long hotelId)
        {
            var hotelReservations = await applicationContext.Reservations
                .Where(r => r.HotelId == hotelId).ToListAsync();

            return hotelReservations;
        }

        public async Task DeleteReservationById(long reservationId)
        {
            var reservation =
                await applicationContext.Reservations.FirstOrDefaultAsync(r => r.ReservationId == reservationId);
            applicationContext.Reservations.Remove(reservation);
            applicationContext.SaveChanges();
            
        }

        public async Task<List<Reservation>> FindReservationByIdAsync(string userId)
        {
            var reservations = await applicationContext.Reservations.Where(a => a.UserId == userId).ToListAsync();

            return reservations;
        }

        public Task DeleteReservationByIdAsync(long reservationId)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteExpiredReservationByIdAsync(string userId)
        {
            var expiredReservation = await applicationContext.Reservations.Where(r=> DateTime.Now.AddDays(-30) >= r.ReservationEnd && r.UserId == userId).ToListAsync();
            foreach (var reservation in expiredReservation)
            {
                applicationContext.Reservations.Remove(reservation);
            }
            
            applicationContext.SaveChanges();
        }
    }
}