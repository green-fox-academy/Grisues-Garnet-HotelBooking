namespace HotelBookingGarnet.Models
{
    public class Guest
    {
        public long GuestId { get; set; }
        public string GuestName { get; set; }
        public long ReservationId { get; set; }
    }
}