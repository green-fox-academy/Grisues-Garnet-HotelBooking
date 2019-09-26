using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace HotelBookingGarnet.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        
        public List<string> ErrorMessages { get; set; } = new List<string>();
        
    }
}