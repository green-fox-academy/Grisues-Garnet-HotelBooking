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
                Seed(context);
                context.SaveChanges();
            }
        }

        public void Dispose()
        {
            using (var context = new ApplicationContext(options))
            {
                context.Reservations.RemoveRange(context.Reservations);
                context.SaveChanges();
            }
        }

        private void Seed(ApplicationContext context)
        {
            context.Reservations.AddRange(new List<Reservation>
            {
                new Reservation
                {
                    ReservationStart = new DateTime(2019, 11, 10),
                    ReservationEnd = new DateTime(2019, 11, 12),
                    ReservationId = 1,
                    RoomId = 5,
                }
            });

            context.Hotels.Add(new Hotel()
            {
                HotelName = "Test",
            });
        }
    }
}