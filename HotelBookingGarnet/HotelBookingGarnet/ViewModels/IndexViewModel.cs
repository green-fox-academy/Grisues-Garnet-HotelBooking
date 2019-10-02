using HotelBookingGarnet.Models;
using System.Collections.Generic;
using HotelBookingGarnet.Utils;
using ReflectionIT.Mvc.Paging;

namespace HotelBookingGarnet.ViewModels
{
    public class IndexViewModel
    {
        public User User { get; set; }
        public Hotel hotel { get; set; }
        public List<Hotel> hotels { get; set; }
        public PropertyType propertyType { get; set;}
        public QueryParam QueryParam { get; set; }
        public PagingList<Hotel> PagingList { get; set; }
    }
}