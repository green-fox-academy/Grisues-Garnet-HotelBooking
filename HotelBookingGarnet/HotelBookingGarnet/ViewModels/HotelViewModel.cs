using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace HotelBookingGarnet.ViewModels
{
    public class HotelViewModel
    {
        [Required]
        public string HotelName { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public int StarRating { get; set; }
        [Required]
        public string PropertyType { get; set; }
        [Required]
        public int Price { get; set; }

        public User User { get; set; }
    }
}