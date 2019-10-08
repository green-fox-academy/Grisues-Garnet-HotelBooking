using HotelBookingGarnet.Models;
using System.Collections.Immutable;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingGarnet
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<Bed> Beds { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<HotelPropertyType> HotelPropertyType { get; set; }
        public DbSet<RoomBed> RoomBed { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        
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
            
            modelBuilder.Entity<RoomBed>()
                .HasKey(a => new {a.RoomId, a.BedId});
            modelBuilder.Entity<RoomBed>()
                .HasOne(a => a.Room)
                .WithMany(a => a.RoomBeds)
                .HasForeignKey(a => a.RoomId);
            modelBuilder.Entity<RoomBed>()
                .HasOne(a => a.Bed)
                .WithMany(a => a.RoomBeds)
                .HasForeignKey(a => a.BedId);
            base.OnModelCreating(modelBuilder);
            
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Name = "Admin", NormalizedName = "Admin".ToUpper()},
                new IdentityRole { Name = "Guest", NormalizedName = "Guest".ToUpper()},
                new IdentityRole { Name = "Hotel Manager", NormalizedName = "Hotel Manager".ToUpper()}
            );
        }
    }
} 