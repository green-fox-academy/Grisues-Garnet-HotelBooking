using HotelBookingGarnet.Models;
using System;
using System.Collections.Generic;
using HotelBookingGarnet.Utils;
using ReflectionIT.Mvc.Paging;

namespace HotelBookingGarnet.ViewModels
{
    public class IndexViewModel
    {
        public User User { get; set; }
        public QueryParam QueryParam { get; set; }
        public PagingList<Hotel> PagingList { get; set; }
        public Hotel Hotel { get; set; }
        public List<ImageDetails> FolderList { get; set; }
        public string ActionName { get; set; }
        public List<RoomBed> RoomBeds { get; set; }
        public List<Reservation> Reservations { get; set; }
        public List<Hotel> HotelList { get; set; }
        public List<Room> Rooms { get; set; }
        public string ErrorMsg { get; set; }
    }
}