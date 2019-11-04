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
        public DateTime? TaxiReservationStart { get; set; }
        [Required(ErrorMessage = "Set the reservation Pick-up local!")]
        public string StartLocal { get; set; }
        [Required(ErrorMessage = "Set the reservation Drop-off local!")]
        public string EndLocal { get; set; }
        [Required(ErrorMessage = "Guest number is required!")]
        public int? NumberOfGuest { get; set; }
        [Required(ErrorMessage = "Phone number is required!")]
        public string PhoneNumber { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();
    }
}
