using HotelBookingGarnet.Models;
using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelBookingGarnet.ViewModels
{
    public class HomeViewModel
    {
        public PagingList<Hotel> PagingList { get; set; }
        public List<ImageDetails> FolderList { get; set; }
    }
}