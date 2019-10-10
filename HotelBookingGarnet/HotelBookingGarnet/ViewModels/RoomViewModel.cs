using System.ComponentModel.DataAnnotations;
using HotelBookingGarnet.Models;

namespace HotelBookingGarnet.ViewModels
{
    public class RoomViewModel
    {
        [Required(ErrorMessage = "The RoomName field is required.")]
        public string RoomName { get; set; }
        [Required] 
        public int Price { get; set; }
        [Required] 
        public int NumberOfGuests { get; set; }
        [Required] 
        public int NumberOfRooms { get; set; }
    }
}