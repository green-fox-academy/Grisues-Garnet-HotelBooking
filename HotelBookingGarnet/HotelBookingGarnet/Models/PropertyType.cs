using System.Collections.Generic;

namespace HotelBookingGarnet.Models
{
    public class PropertyType
    {
        public long PropertyTypeId { get; set; }
        public string Type { get; set; }
        public ICollection<HotelPropertyType> HotelPropertyTypes { get; set; }
    }
}