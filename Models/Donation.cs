using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gift_of_Givers.Models
{
    public class Donation
    {
        public int DonationId { get; set; }

        // Links the donation to the signed-in user
        [Required]
        public string UserId { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter a donor name.")]
        [Display(Name = "Donor Name")]
        public string DonorName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter an email address.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        [Display(Name = "Email")]
        public string DonorEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter an amount.")]
        [Range(1, 1_000_000, ErrorMessage = "Amount must be at least 1.")]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Donation Amount")]
        public decimal Amount { get; set; }

        [Required]
        public string Currency { get; set; } = "ZAR";

        [Required]
        [Display(Name = "Donation Type")]
        public string DonationType { get; set; } = "One-time";

        public DateTime DonatedAt { get; set; } = DateTime.UtcNow;

        // Navigation
        public ApplicationUser? User { get; set; }
    }
}