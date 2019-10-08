using HotelBookingGarnet.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingGarnet.Services
{
    interface IReservation
    {
        Task<Reservation> FindReservationByIdAsync(String UserId);
    }
}
