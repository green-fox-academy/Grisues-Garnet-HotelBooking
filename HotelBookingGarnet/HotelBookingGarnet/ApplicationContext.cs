using HotelBookingGarnet.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingGarnet
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "Admin", NormalizedName = "Admin".ToUpper()},
                new IdentityRole { Name = "Guest", NormalizedName = "Guest".ToUpper()},
                new IdentityRole { Name = "Hotel Manager", NormalizedName = "Hotel Manager".ToUpper()}
                ); 
        }
        public DbSet<Hotel> Hotels { get; set; }
    }
}