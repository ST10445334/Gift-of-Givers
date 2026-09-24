using System.ComponentModel.DataAnnotations;

namespace Gift_of_Givers.Models
{
    public class Volunteer
    {
        public int VolunteerId { get; set; }

        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your full name.")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter an email address.")]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a phone number.")]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; } = string.Empty;

        [StringLength(500)]
        [Display(Name = "Skills / Areas of Interest")]
        public string? Skills { get; set; }

        [StringLength(200)]
        [Display(Name = "Availability")]
        public string? Availability { get; set; }

        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ApplicationUser? User { get; set; }
    }
}