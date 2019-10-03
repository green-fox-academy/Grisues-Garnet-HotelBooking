using System.ComponentModel.DataAnnotations;

namespace HotelBookingGarnet.ViewModels
{
    public class BedViewModel
    {
        [Required] [Range(1, 10)] public int NumberOfBeds { get; set; } = 1;
        [Required]
        public string BedType { get; set; }
        
    }
}