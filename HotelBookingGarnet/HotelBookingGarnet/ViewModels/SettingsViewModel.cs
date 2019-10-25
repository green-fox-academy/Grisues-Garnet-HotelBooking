using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HotelBookingGarnet.Models;
using Microsoft.CodeAnalysis.Options;

namespace HotelBookingGarnet.ViewModels
{
    public class SettingsViewModel
    {
        public User User { get; set; }
        
        [Display(Name = "Current password")]
        [Required(ErrorMessage = "Your current password is required!")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        
        [Display(Name = "New password")]
        [Required(ErrorMessage = "New password is required!")]
        [DataType(DataType.Password)]
        [StringLength(20, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.",
        MinimumLength = 6)]
        [Compare("OldPassword", ErrorMessage = "New password and old password can't be the same!")]
        public string NewPassword { get; set; }
        
        [Display(Name = "Confirm password")]
        [Required(ErrorMessage = "Confirmation password is required!")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Confirmation password and new password doesn't match")]
        public string ConfirmPassword { get; set; }
        
        //public List<string> ErrorMessages { get; set; }
    }
}