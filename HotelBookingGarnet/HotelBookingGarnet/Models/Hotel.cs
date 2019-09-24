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
        public string PropertyType { get; set; }
        public int Price { get; set; }
        public long UserId { get; set; }
    }
}