using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingGarnet.Models
{
    public class TaxiReservation
    {
        public long TaxiReservationId { get; set; }
        public DateTime TaxiReservationStart { get; set; }
        public DateTime TaxiReservationEnd { get; set; }
        public int NumberOfGuest { get; set; }
        public string PhoneNumber { get; set; }
        public string UserId { get; set; }
    }
}
