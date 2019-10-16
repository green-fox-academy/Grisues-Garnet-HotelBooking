using System.Collections.Generic;
using System.Drawing.Printing;
using System.Threading.Tasks;
using HotelBookingGarnet.Models;
using HotelBookingGarnet.ViewModels;
using Microsoft.CodeAnalysis.CSharp;

namespace HotelBookingGarnet.Services
{
    public class GuestService : IGuestService
    {
        private readonly ApplicationContext applicationContext;

        public GuestService(ApplicationContext applicationContext)
        {
            this.applicationContext = applicationContext;
        }

        public async Task SaveGuestAsync(ReservationViewModel newReservation, long reservationId)
        {
            var guestNameArray = SplitGuestNameString(newReservation);
            foreach (var name in guestNameArray)
            {
                var guest = new Guest
                {
                    GuestName = name,
                    ReservationId = reservationId
                };
                await applicationContext.Guests.AddAsync(guest);
            }

            await applicationContext.SaveChangesAsync();
        }

        public IEnumerable<string> SplitGuestNameString(ReservationViewModel newReservation)
        {
            var guestNameString = newReservation.GuestNames.Split(", ");
            return guestNameString;
        }
    }
}