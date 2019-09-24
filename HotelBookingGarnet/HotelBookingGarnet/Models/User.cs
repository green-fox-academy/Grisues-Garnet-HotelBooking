using System.Collections.Generic;

namespace HotelBookingGarnet.Models
{
    public class User
    {
        public long UserId { get; set; }
        public List<Hotel> Hotels { get; set; }
    }
}