using HotelBookingGarnet.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingGarnet
{
    public class ApplicationContext : DbContext
    {

        public DbSet<Hotel> Hotels { get; set; }

        public ApplicationContext(DbContextOptions options) : base(options)
        {

        }
    }
} 