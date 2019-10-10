using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingGarnet.ViewModels
{
    public class ReservationViewModel
    {
        [Required]
        public DateTime ReservationStart { get; set; }
        [Required] 
        public DateTime ReservationEnd { get; set; }
        [Required] 
        public int NumberOfGuest { get; set; }
        public string PhoneNumber { get; set; }
        [Required] 
        public string GuestsNameInString { get; set; }
    }
}