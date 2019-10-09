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
            var hotelReservations = await applicationContext.Reservations.Include(r => r.RoomId)
                .Where(r => r.HotelId == hotelId).ToListAsync();

            return hotelReservations;
        }
    }
}