using System.Collections.Generic;

namespace HotelBookingGarnet.Models
{
    public class Room
    {
        public long RoomId { get; set; }
        public string RoomName { get; set; }
        public int Price { get; set; }
        public int NumberOfGuests { get; set; }
        public int NumberOfRooms { get; set; }
        public long HotelId { get; set; }
        public ICollection<RoomBed> RoomBeds { get; set; }
        public int NumberOfAvailablePlaces { get; set; }
    }
}