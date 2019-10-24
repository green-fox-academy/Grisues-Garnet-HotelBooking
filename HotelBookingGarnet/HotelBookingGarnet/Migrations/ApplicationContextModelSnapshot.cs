﻿// <auto-generated />
using System;
using HotelBookingGarnet;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace HotelBookingGarnet.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("HotelBookingGarnet.Models.Bed", b =>
                {
                    b.Property<long>("BedId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("BedType");

                    b.Property<int>("NumberOfBeds");

                    b.HasKey("BedId");

                    b.ToTable("Beds");
                });

            modelBuilder.Entity("HotelBookingGarnet.Models.Guest", b =>
                {
                    b.Property<long>("GuestId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("GuestName");

                    b.Property<long>("ReservationId");

                    b.HasKey("GuestId");

                    b.HasIndex("ReservationId");

                    b.ToTable("Guests");
                });

            modelBuilder.Entity("HotelBookingGarnet.Models.Hotel", b =>
                {
                    b.Property<long>("HotelId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address");

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("Description");

                    b.Property<string>("HotelName");

                    b.Property<bool>("IsItAvailable");

                    b.Property<int>("Price");

                    b.Property<string>("Region");

                    b.Property<int>("StarRating");

                    b.Property<string>("TimeZone");

                    b.Property<string>("Uri");

                    b.Property<string>("UserId");

                    b.HasKey("HotelId");

                    b.HasIndex("UserId");

                    b.ToTable("Hotels");
                });

            modelBuilder.Entity("HotelBookingGarnet.Models.HotelPropertyType", b =>
                {
                    b.Property<long>("HotelId");

                    b.Property<long>("PropertyTypeId");

                    b.HasKey("HotelId", "PropertyTypeId");

                    b.HasIndex("PropertyTypeId");

                    b.ToTable("HotelPropertyType");
                });

            modelBuilder.Entity("HotelBookingGarnet.Models.PropertyType", b =>
                {
                    b.Property<long>("PropertyTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Type");

                    b.HasKey("PropertyTypeId");

                    b.ToTable("PropertyTypes");
                });

            modelBuilder.Entity("HotelBookingGarnet.Models.Reservation", b =>
                {
                    b.Property<long>("ReservationId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("HotelId");

                    b.Property<int>("NumberOfGuest");

                    b.Property<string>("PhoneNumber");

                    b.Property<DateTime>("ReservationEnd");

                    b.Property<DateTime>("ReservationStart");

                    b.Property<long>("RoomId");

                    b.Property<int>("TotalPrice");

                    b.Property<string>("UserId");

                    b.HasKey("ReservationId");

                    b.HasIndex("HotelId");

                    b.HasIndex("UserId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("HotelBookingGarnet.Models.Review", b =>
                {
                    b.Property<long>("ReviewId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("HotelId");

                    b.Property<int>("Rating");

                    b.Property<string>("Text");

                    b.Property<string>("UserId");

                    b.HasKey("ReviewId");

                    b.HasIndex("HotelId");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("HotelBookingGarnet.Models.Room", b =>
                {
                    b.Property<long>("RoomId")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("HotelId");

                    b.Property<int>("NumberOfAvailablePlaces");

                    b.Property<int>("NumberOfGuests");

                    b.Property<int>("NumberOfRooms");

                    b.Property<int>("Price");

                    b.Property<string>("RoomName");

                    b.HasKey("RoomId");

                    b.HasIndex("HotelId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("HotelBookingGarnet.Models.RoomBed", b =>
                {
                    b.Property<long>("RoomId");

                    b.Property<long>("BedId");

                    b.HasKey("RoomId", "BedId");

                    b.HasIndex("BedId");

                    b.ToTable("RoomBed");
                });

            modelBuilder.Entity("HotelBookingGarnet.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");

                    b.HasData(
                        new
                        {
                            Id = "1daa2e84-5962-4351-b5b6-d396f9af5c09",
                            ConcurrencyStamp = "35687d80-0283-4c69-be45-ab3534913189",
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        },
                        new
                        {
                            Id = "a17bc994-4fbb-4259-a3eb-656e942807b5",
                            ConcurrencyStamp = "af7dd3c6-d704-4db7-82f2-0b19b66e2da7",
                            Name = "Guest",
                            NormalizedName = "GUEST"
                        },
                        new
                        {
                            Id = "11f145ec-bc6c-49d8-95e1-362e84366b6e",
                            ConcurrencyStamp = "18585420-0041-490a-95fa-9e3b55230a51",
                            Name = "Hotel Manager",
                            NormalizedName = "HOTEL MANAGER"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("HotelBookingGarnet.Models.Guest", b =>
                {
                    b.HasOne("HotelBookingGarnet.Models.Reservation")
                        .WithMany("GuestsList")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HotelBookingGarnet.Models.Hotel", b =>
                {
                    b.HasOne("HotelBookingGarnet.Models.User")
                        .WithMany("Hotels")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("HotelBookingGarnet.Models.HotelPropertyType", b =>
                {
                    b.HasOne("HotelBookingGarnet.Models.Hotel", "Hotel")
                        .WithMany("HotelPropertyTypes")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HotelBookingGarnet.Models.PropertyType", "PropertyType")
                        .WithMany("HotelPropertyTypes")
                        .HasForeignKey("PropertyTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HotelBookingGarnet.Models.Reservation", b =>
                {
                    b.HasOne("HotelBookingGarnet.Models.Hotel")
                        .WithMany("HotelReservations")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HotelBookingGarnet.Models.User")
                        .WithMany("UserReservations")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("HotelBookingGarnet.Models.Review", b =>
                {
                    b.HasOne("HotelBookingGarnet.Models.Hotel")
                        .WithMany("Reviews")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HotelBookingGarnet.Models.Room", b =>
                {
                    b.HasOne("HotelBookingGarnet.Models.Hotel")
                        .WithMany("Rooms")
                        .HasForeignKey("HotelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("HotelBookingGarnet.Models.RoomBed", b =>
                {
                    b.HasOne("HotelBookingGarnet.Models.Bed", "Bed")
                        .WithMany("RoomBeds")
                        .HasForeignKey("BedId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HotelBookingGarnet.Models.Room", "Room")
                        .WithMany("RoomBeds")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("HotelBookingGarnet.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("HotelBookingGarnet.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("HotelBookingGarnet.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("HotelBookingGarnet.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
