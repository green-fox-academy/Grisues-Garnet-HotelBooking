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
                SeedPosts(context);
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

        private void SeedPosts(ApplicationContext context)
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
    }
}