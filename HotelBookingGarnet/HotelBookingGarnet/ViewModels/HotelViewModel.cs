using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HotelBookingGarnet.Models;
using Microsoft.AspNetCore.Http;
using TimeZoneInfo = System.TimeZoneInfo;

namespace HotelBookingGarnet.ViewModels
{
    public class HotelViewModel
    {
        [Required(ErrorMessage = "The HotelName field is required.")]
        public string HotelName { get; set; }
        [Required(ErrorMessage = "The Country field is required.")]
        public string Country { get; set; }
        [Required(ErrorMessage = "The Region field is required.")]

        public string Region { get; set; }
        [Required(ErrorMessage = "The City field is required.")]
        public string City { get; set; }
        [Required(ErrorMessage = "The Address field is required.")]
        public string Address { get; set; }
        [Required(ErrorMessage = "The Description field is required.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "The StarRating field is required.")]
        public int StarRating { get; set; }
        [Required(ErrorMessage = "The PropertyType field is required.")]
        public string PropertyType { get; set; }
        public User User { get; set; }
        public IFormFileCollection Files { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();
        public string TimeZone { get; set; }
    }
}