using System.Collections.Generic;

namespace HotelBookingGarnet.Models
{
    public class User
    {
        public long UserId { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public List<Hotel> Hotels { get; set; }
        
    }
}