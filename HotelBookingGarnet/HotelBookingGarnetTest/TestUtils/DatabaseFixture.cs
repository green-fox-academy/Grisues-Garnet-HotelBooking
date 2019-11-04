using System;
using System.Collections.Generic;
using HotelBookingGarnet;
using HotelBookingGarnet.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingGarnetTest.TestUtils
{
    public class DatabaseFixture : IDisposable
    {
        private readonly DbContextOptions<ApplicationContext> options;

        public DatabaseFixture()
        {
            options = TestDbOptions.Get();
            using (var context = new ApplicationContext(options))
            {
                SeedReservation(context);
                SeedTaxiReservation(context);
                context.SaveChanges();
            }
        }

        public void Dispose()
        {
            using (var context = new ApplicationContext(options))
            {
                context.Reservations.RemoveRange(context.Reservations);
                context.Rooms.RemoveRange(context.Rooms);
                context.SaveChanges();
            }
        }

        private void SeedReservation(ApplicationContext context)
        {
            context.Reservations.AddRange(new List<Reservation>
            {
                new Reservation
                {
                    ReservationStart = new DateTime(2019, 11, 10),
                    ReservationEnd = new DateTime(2019, 11, 12),
                    ReservationId = 1,
                }
            });
        }
        private void SeedTaxiReservation(ApplicationContext context)
        {
            context.TaxiReservations.AddRange(new List<TaxiReservation>
            {
                new TaxiReservation
                {
                    TaxiReservationStart = new DateTime(2019,11,10),
                    TaxiReservationId = 1,
                    NumberOfGuest = 2,
                    PhoneNumber = "222222222",
                    StartLocal = "Budapest",
                    EndLocal = "Siófok",
                    UserId = "1"
                }
            });
        }
    }
}