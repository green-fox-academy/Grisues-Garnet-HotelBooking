using HotelBookingGarnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingGarnet.Services
{
   public interface IReservationService
    {
        Task<List<Reservation>> FindReservationByIdAsync(String UserId);

        void DeleteReservationById(long ReservationId);
    }
}
