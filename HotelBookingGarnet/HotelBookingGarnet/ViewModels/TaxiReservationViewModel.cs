using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingGarnet.ViewModels
{
    public class TaxiReservationViewModel
    {
        [Required(ErrorMessage = "Set the reservation start date!")]
        public DateTime TaxiReservationStart { get; set; }
        [Required(ErrorMessage = "Set the reservation end date!")]
        public DateTime TaxiReservationEnd { get; set; }
        [Required(ErrorMessage = "Guest number is required!")]
        public int TaxiNumberOfGuest { get; set; }
        [Required(ErrorMessage = "Phone number is required!")]
        public string PhoneNumber { get; set; }
    }
}
