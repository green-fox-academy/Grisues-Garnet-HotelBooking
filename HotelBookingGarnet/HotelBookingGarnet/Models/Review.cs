namespace HotelBookingGarnet.Models
{
    public class Review
    {
        public long ReviewId { get; set; }
        public int Rating { get; set; }
        public string Text { get; set; }
        public long HotelId { get; set; }
        public string UserId { get; set; }
    }
}