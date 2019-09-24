using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingGarnet.Models
{
    public class User
    {
        public long UserId { get; set; }
        public string Role { get; set; }
        public List<Hotel> Hotels { get; set; }
    }
}
