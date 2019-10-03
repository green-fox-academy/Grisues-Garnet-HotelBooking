using HotelBookingGarnet.Models;
using System;
using System.Collections.Generic;

namespace HotelBookingGarnet.ViewModels
{
    public class IndexViewModel
    {
        public User User { get; set; }
        public Hotel hotel { get; set; }
        public List<Hotel> hotels { get; set; }
        public PropertyType propertyType { get; set; }
        public IEnumerable<Uri> blobs { get; set; }
    }
}