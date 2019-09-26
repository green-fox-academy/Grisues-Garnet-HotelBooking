namespace HotelBookingGarnet.Models
{
    public class HotelPropertyType
    {
        public long HotelId { get; set; }
        public long PropertyTypeId { get; set; }
        public Hotel Hotel { get; set; }
        public PropertyType PropertyType { get; set; }
    }
}