using System.ComponentModel.DataAnnotations;

namespace AksDemoApp.Models
{
    public class ChangePassword
    {
     

        [Required(ErrorMessage = "Password is required.")]
        [Display (Name ="Current Password")]
        public string oldPassword { get; set; }
        [Required, Display(Name = "New Password")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirmation Password is required.")]
        [Compare("NewPassword", ErrorMessage = "Password and Confirmation Password must match.")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
    
     }
}
