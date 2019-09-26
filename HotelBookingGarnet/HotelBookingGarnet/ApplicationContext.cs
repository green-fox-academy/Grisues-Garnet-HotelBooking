using System.Collections.Immutable;
using HotelBookingGarnet.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingGarnet
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Hotel> Hotels { get; set; }

        public DbSet<PropertyType> PropertyTypes { get; set; }
        
        public ApplicationContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HotelPropertyType>()
                .HasKey(a => new {a.HotelId, a.PropertyTypeId});
            modelBuilder.Entity<HotelPropertyType>()
                .HasOne(a => a.Hotel)
                .WithMany(a => a.HotelPropertyTypes)
                .HasForeignKey(a => a.HotelId);
            modelBuilder.Entity<HotelPropertyType>()
                .HasOne(a => a.PropertyType)
                .WithMany(a => a.HotelPropertyTypes)
                .HasForeignKey(a => a.PropertyTypeId);
            base.OnModelCreating(modelBuilder);
            
        }
    }
}