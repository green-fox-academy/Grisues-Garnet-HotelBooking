using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingGarnet.DTOs
{
    public class APITaxiReservationDTO
    {
        public DateTime? TaxiReservationStart { get; set; }
        public string StartLocal { get; set; }
        public string EndLocal { get; set; }
        public int? NumberOfGuest { get; set; }
        public string PhoneNumber { get; set; }
    }
}
