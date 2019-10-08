using System.Collections.Generic;

namespace HotelBookingGarnet.Models
{
    public class Hotel
    {
        public long HotelId { get; set; }
        public string HotelName { get; set; }
        public string Country { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public int StarRating { get; set; }
        public int Price { get; set; }
        public string UserId { get; set; }
        public List<Room> Rooms { get; set; }
        public ICollection<HotelPropertyType> HotelPropertyTypes { get; set; }
        public string Uri { get; set; }
        public bool IsItAvailable { get; set; }
        public List<Reservation> Userreservations { get; set; }
    }
}