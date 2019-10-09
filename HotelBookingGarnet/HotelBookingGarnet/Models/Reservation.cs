using System;
using System.Collections.Generic;

namespace HotelBookingGarnet.Models
{
    public class Reservation
    {
        public long ReservationId { get; set; }
        public int TotalPrice { get; set; }
        public int NumberOfGuest { get; set; }
        public string PhoneNumber { get; set; }
        //public List<Guest> NameOfGuests { get; set; }
        public long RoomId { get; set; }
        public DateTime ReservationStart { get; set; }
        public DateTime ReservationEnd { get; set; }
        public string UserId { get; set; }
        public long HotelId { get; set; }
    }
}