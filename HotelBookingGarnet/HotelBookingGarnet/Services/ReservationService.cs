using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingGarnet.Services
{
    public class ReservationService : IReservation
    {
        protected readonly ApplicationContext applicationContext;

        public ReservationService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task<Reservation> FindReservationByIdAsync(string UserId)
        {
            
          var reservations =  await applicationContext.Reservation.FirstOrDefaultAsync(r => r.UserId == UserId);
            return reservations;
        }
    }
}
