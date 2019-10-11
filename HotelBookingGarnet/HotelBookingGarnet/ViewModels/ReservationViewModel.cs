using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HotelBookingGarnet.ViewModels
{
    public class ReservationViewModel
    {
        [Required(ErrorMessage = "Set the reservation start date!")]
        public DateTime ReservationStart { get; set; }

        [Required(ErrorMessage = "Set the reservation end date!")]
        public DateTime ReservationEnd { get; set; }
        [Required]
        [Range(1,100,ErrorMessage="Please enter a correct value!")]
        public int NumberOfGuest { get; set; }
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Please give all the guests name!")]
        public string GuestsNameInString { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();
    }
}