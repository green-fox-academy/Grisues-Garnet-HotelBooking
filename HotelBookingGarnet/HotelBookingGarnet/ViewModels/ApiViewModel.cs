using System.Collections.Generic;
using HotelBookingGarnet.Models;

namespace HotelBookingGarnet.ViewModels
{
    public class ApiViewModel
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public List<Hotel> HotelList { get; set; }
    }
}