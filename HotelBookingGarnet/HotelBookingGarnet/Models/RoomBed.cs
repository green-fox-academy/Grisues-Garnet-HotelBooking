namespace HotelBookingGarnet.Models
{
    public class RoomBed
    {
        public long BedId { get; set; }
        public Bed Bed { get; set; }
        public long RoomId { get; set; }
        public Room Room { get; set; }
    }
}