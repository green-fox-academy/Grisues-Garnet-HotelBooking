using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingGarnet.ViewModels
{
    public class ReservationViewModel
    {
        [Required(ErrorMessage = "Set the reservation start date!")]
        public DateTime? FromDate { get; set; }

        [Required(ErrorMessage = "Set the reservation end date!")]
        public DateTime? ToDate { get; set; }
        
        [Required(ErrorMessage = "Guest number is required!")]
        public int? NumberGuests { get; set; }
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please give all the guests name!")]
        public string GuestNames { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();
    }
}