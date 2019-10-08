using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HotelBookingGarnet.Models;
using Microsoft.AspNetCore.Http;

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
        public User User { get; set; }
        public IFormFileCollection Files { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();
    }
}