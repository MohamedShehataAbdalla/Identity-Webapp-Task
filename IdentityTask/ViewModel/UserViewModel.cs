using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace IdentityTask.ViewModel
{
    public class UserViewModel
    {
        [Key]
        [MaxLength(450)]
        public string Id { get; set; } = String.Empty;

        [Required]
        [MaxLength(255)]
        [Display(Name = "Name")]
        public string? UserName { get; set; }

        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; } = String.Empty;

        [Display(Name = "Email Confirmed")]
        public bool EmailConfirmed { get; set; }
        public string Password { get; set; } = String.Empty;

        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; } = String.Empty;

        [MaxLength(11)]
        [MinLength(11)]
        [Display(Name = "Phone Number")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Phone Confirmed")]
        public bool PhoneNumberConfirmed { get; set; }


        public IEnumerable<IdentityUser>? Users { get; set; }
    }
}
