using System.ComponentModel.DataAnnotations;

namespace HotelBookingGarnet.ViewModels
{
    public class HotelViewModel
    {
        [Required]
        public string HotelName { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Region { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [Range(1, 5)]
        public int StarRating { get; set; }
        [Required]
        public string PropertyType { get; set; }
        
    }
}