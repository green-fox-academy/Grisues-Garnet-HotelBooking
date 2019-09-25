using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingGarnet.Models
{
    public class User:IdentityUser
    {
        public string Role { get; set; }
    }
}
