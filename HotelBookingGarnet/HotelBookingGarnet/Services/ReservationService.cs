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
        protected readonly ApplicationContext applicationContext;

        public ReservationService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task<List<Reservation>> FindReservationByIdAsync(string userId)
        {
            var reservations = await applicationContext.Reservation.Where(a => a.UserId == userId).ToListAsync();
            return reservations;

        }
    }
}
