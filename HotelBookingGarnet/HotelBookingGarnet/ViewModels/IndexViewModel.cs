using HotelBookingGarnet.Models;
using System;
using System.Collections.Generic;

namespace HotelBookingGarnet.ViewModels
{
    public class IndexViewModel
    {
        public User User { get; set; }
        public Hotel Hotel { get; set; }
        public List<ImageDetails> blobs { get; set; }
        public List<RoomBed> RoomBeds { get; set; }

    }
}