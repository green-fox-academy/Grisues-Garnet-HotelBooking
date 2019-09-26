using HotelBookingGarnet.Models;

namespace HotelBookingGarnet.ViewModels
{
    public class IndexViewModel
    {
        public User User { get; set; }

        public Hotel hotel { get; set; }
        public PropertyType propertyType { get; set; }
    }
}