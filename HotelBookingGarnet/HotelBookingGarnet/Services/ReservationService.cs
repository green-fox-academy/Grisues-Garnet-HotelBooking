using HotelBookingGarnet.ViewModels;
using AutoMapper;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;
using FluentEmail.Core;
using FluentEmail.Mailgun;
using HotelBookingGarnet.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelBookingGarnet.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ApplicationContext applicationContext;
        private readonly IMapper mapper;
        private readonly IGuestService guestService;
        private readonly IRoomService roomService;
        private readonly IUserService userService;
        private readonly IHotelService hotelService;

        public ReservationService(IMapper mapper, ApplicationContext applicationContext, IGuestService guestService,
            IRoomService roomService, IUserService userService, IHotelService hotelService)
        {
            this.applicationContext = applicationContext;
            this.mapper = mapper;
            this.guestService = guestService;
            this.roomService = roomService;
            this.userService = userService;
            this.hotelService = hotelService;
        }

        public async Task<List<Reservation>> FindReservationsByHotelIdAsync(long hotelId)
        {
            var hotelReservations = await applicationContext.Reservations.Include(r => r.GuestsList)
                .Where(r => r.HotelId == hotelId).OrderBy(a => a.ReservationStart).ToListAsync();
            return hotelReservations;
        }

        public async Task<List<Reservation>> FindReservationsByRoomIdAsync(long roomId)
        {
            var hotelReservations = await applicationContext.Reservations.Include(r => r.GuestsList)
                .Where(r => r.RoomId == roomId).OrderBy(a => a.ReservationStart).ToListAsync();
            return hotelReservations;
        }

        public async Task<List<Reservation>> FindReservationByReservationIdAsync(string userId)
        {
            var reservations = await applicationContext.Reservations.Include(r => r.GuestsList)
                .Where(a => a.UserId == userId).OrderBy(a => a.ReservationStart).ToListAsync();

            return reservations;
        }

        public async Task<Reservation> FindReservationByReservationIdAsync(long reservationId)
        {
            var foundReservation = await applicationContext.Reservations.Include(p => p.GuestsList)
                .FirstOrDefaultAsync(a => a.ReservationId == reservationId);
            return foundReservation;
        }

        public async Task DeleteExpiredReservationByIdAsync(string userId)
        {
            var expiredReservation = await applicationContext.Reservations
                .Where(r => DateTime.Now.AddDays(-30) >= r.ReservationEnd && r.UserId == userId).ToListAsync();
            foreach (var reservation in expiredReservation)
            {
                applicationContext.Reservations.Remove(reservation);
            }

            applicationContext.SaveChanges();
        }

        public async Task DeleteReservationByIdAsync(long reservationId)
        {
            var reservation =
                await applicationContext.Reservations.FirstOrDefaultAsync(r => r.ReservationId == reservationId);
            applicationContext.Reservations.Remove(reservation);
            applicationContext.SaveChanges();
        }

        public async Task<long> AddReservationAsync(ReservationViewModel newReservation, string userId, long roomId,
            long hotelId)
        {
            var reservation = mapper.Map<ReservationViewModel, Reservation>(newReservation);
            reservation.UserId = userId;
            reservation.RoomId = roomId;
            reservation.HotelId = hotelId;
            reservation.TotalPrice = await CalculatePriceAsync(roomId, reservation);
            await applicationContext.Reservations.AddAsync(reservation);
            await applicationContext.SaveChangesAsync();
            await guestService.SaveGuestAsync(newReservation, reservation.ReservationId);
            await SendEmailAsync(hotelId, reservation);

            return reservation.ReservationId;
        }

        private async Task SendEmailAsync(long hotelId, Reservation reservation)
        {
            var sender = new MailgunSender(
                "sandbox0ec3cdedf8584e3fa03c7b70b98fc52f.mailgun.org",
                "869a1d058062aee81f0348cb5cd5ace5-2dfb0afe-68088ff5"
            );
            Email.DefaultSender = sender;

            var user = await userService.FindUserByHotelIdAsync(hotelId);
            var hotel = await hotelService.FindHotelByIdAsync(hotelId);

            var template =
                "You received this message because one of your hotel's room has been booked in the Garnet Travel " +
                "reservation system. \r\n\n Hotel name: " + hotel.HotelName +
                "\r\n Room number: " + reservation.RoomId +
                "\r\n Reservation start: " + reservation.ReservationStart +
                "\r\n Reservation end: " + reservation.ReservationEnd +
                "\r\n Number of guests: " + reservation.NumberOfGuest +
                "\r\n Total price: " + reservation.TotalPrice +
                "\r\n Reservations for this hotel: https://garnettravel.azurewebsites.net/hotelReservation/" +
                hotel.HotelId +
                "\r\n\n Sincerely, Garnet Travel team" +
                "\r\n (This is an auto generated message, please do not reply!)";

            var email = Email
                .From("mailgun@sandbox0ec3cdedf8584e3fa03c7b70b98fc52f.mailgun.org", "GarnetTravel.Info")
                .To(user.Email)
                .Subject($"Reservation notification #{reservation.ReservationId}")
                .UsingTemplate(template, false, false);

            await email.SendAsync();
        }

        private async Task<int> CalculatePriceAsync(long roomId, Reservation reservation)
        {
            var room = await roomService.FindRoomByIdAsync(roomId);
            var daysOfReservation = (int) (reservation.ReservationEnd - reservation.ReservationStart).TotalDays + 1;
            return room.Price * daysOfReservation;
        }

        public async Task<List<string>> ReservationValidationAsync(ReservationViewModel newReservation, long roomId)
        {
            var dateValid = DateValidation(newReservation);
            var occupationValid = await OccupationValidationAsync(newReservation, roomId);
            var guestValid = GuestNumberValidation(newReservation, roomId);

            if (dateValid != null)
                newReservation.ErrorMessages.Add(dateValid);
            if (occupationValid != null)
                newReservation.ErrorMessages.Add(occupationValid);
            if (guestValid != null)
                newReservation.ErrorMessages.Add(guestValid);

            return newReservation.ErrorMessages;
        }

        private static string GuestNumberValidation(ReservationViewModel newReservation, long roomId)
        {
            var guestNameListSize = newReservation.GuestsNameInString.Split(", ").Length;
            return newReservation.NumberOfGuest != guestNameListSize
                ? "The specified guests does not match with the given guest number!"
                : null;
        }

        private async Task<string> OccupationValidationAsync(ReservationViewModel newReservation, long roomId)
        {
            var reservationCount = 0;
            var startDate = newReservation.ReservationStart;
            var endDate = newReservation.ReservationEnd;

            foreach (var reservation in FindReservationsByRoomIdAsync(roomId).Result)
            {
                var occupationStart = reservation.ReservationStart;
                var occupationEnd = reservation.ReservationEnd;

                if (occupationStart <= startDate && startDate <= occupationEnd ||
                    occupationStart <= endDate && endDate <= occupationEnd ||
                    startDate <= occupationStart && occupationEnd <= endDate)
                {
                    var styledStartDate = reservation.ReservationStart.ToString("yyyy/MM/dd");
                    var styledEndDate = reservation.ReservationEnd.ToString("yyyy/MM/dd");
                    reservationCount++;
                    var room = await roomService.FindRoomByIdAsync(roomId);
                    if (room.NumberOfRooms == reservationCount)
                    {
                        return $"Room is already booked in this interval:  {styledStartDate} - {styledEndDate}";
                    }
                }
            }

            return null;
        }

        private static string DateValidation(ReservationViewModel newReservation)
        {
            var startDate = newReservation.ReservationStart;
            var endDate = newReservation.ReservationEnd;

            if (startDate > endDate)
            {
                return "The end of the booking must not precede the start time!";
            }

            return startDate < DateTime.Today ? "The booking cannot begin earlier than today!" : null;
        }

        public async Task<List<Room>> FindAvailableRoomByHotelIdAndDateAsync(long hotelId, DateTime start, DateTime end)
        {
            var hotel = await hotelService.FindHotelByIdAsync(hotelId);

            var reservations = await applicationContext.Reservations.Where(r =>
                r.HotelId == hotelId && start <= r.ReservationStart && r.ReservationStart <= end ||
                start <= r.ReservationEnd && r.ReservationEnd <= end ||
                r.ReservationStart <= start && end <= r.ReservationEnd).ToListAsync();

            Dictionary<long, int> roomsReservation = new Dictionary<long, int>();

            if (reservations.Count == 0)
            {
                return null;
            }
            else
            {
                List<long> roomId = ListReservationsById(reservations);
                roomsReservation = FillRoomsReservationDictionaryWithReservationsRoomId(roomId);
                var rooms = await roomService.FindRoomByHotelIdAsync(hotelId);
                List<Room> filteredRooms = FilterAvailableRooms(rooms, roomsReservation);

                return filteredRooms;
            }
        }

        private List<Room> FilterAvailableRooms(List<Room> rooms, Dictionary<long, int> roomsReservation)
        {
            List<Room> filteredRooms = new List<Room>();
            for (int i = 0; i < rooms.Count; i++)
            {
                if (rooms.ElementAt(i).NumberOfRooms > roomsReservation[rooms.ElementAt(i).RoomId])
                {
                    filteredRooms.Add(rooms.ElementAt(i));
                }
            }

            return filteredRooms;
        }

        private Dictionary<long, int> FillRoomsReservationDictionaryWithReservationsRoomId(List<long> roomId)
        {
            Dictionary<long, int> roomsReservation = new Dictionary<long, int>();
            for (int i = 0; i < roomId.Count; i++)
            {
                if (!roomsReservation.ContainsKey(roomId.ElementAt(i)))
                {
                    roomsReservation.Add(roomId.ElementAt(i), 1);
                }
                else
                {
                    roomsReservation[roomId.ElementAt(i)]++;
                }
            }

            return roomsReservation;
        }

        private List<long> ListReservationsById(List<Reservation> reservations)
        {
            List<long> tempList = new List<long>();
            for (int i = 0; i < reservations.Count; i++)
            {
                tempList.Add(reservations.ElementAt(i).RoomId);
            }

            return tempList;
        }
    }
}