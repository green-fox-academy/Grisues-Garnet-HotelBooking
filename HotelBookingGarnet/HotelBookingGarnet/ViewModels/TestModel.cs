using HotelBookingGarnet.Models;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingGarnet.ViewModels
{
    public class TestModel
    {
        public PagingList<Hotel> Hotels { get; set; }
    }
}
