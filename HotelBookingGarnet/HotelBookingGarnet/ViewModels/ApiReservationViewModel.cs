using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingGarnet.ViewModels
{
    public class ApiReservationViewModel
    {
        [Required]
        public int HotelId { get; set; }
        [Required]
        public int RoomId { get; set; }
        [Required]
        public DateTime FromDate { get; set; }
        [Required]
        public DateTime ToDate { get; set; }
        [Required]
        public int NumberGuests { get; set; }
        [Required]
        public string GuestNames { get; set; }
    }
}