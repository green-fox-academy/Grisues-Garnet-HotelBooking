using System.Collections.Generic;

namespace HotelBookingGarnet.Models
{
    public class Bed
    {
        public long BedId { get; set; }
        public int NumberOfBeds { get; set; }
        public string BedType { get; set; }
        public ICollection<RoomBed> RoomBeds { get; set; }
    }
}