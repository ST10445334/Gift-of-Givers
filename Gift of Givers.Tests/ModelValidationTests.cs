using System.ComponentModel.DataAnnotations;
using Gift_of_Givers.Models;

namespace Gift_of_Givers.Tests
{
    public class ModelValidationTests
    {
        private static List<ValidationResult> Validate(object model)
        {
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(model, new ValidationContext(model), results, validateAllProperties: true);
            return results;
        }

        private static Donation ValidDonation() => new()
        {
            UserId = "user-1",
            DonorName = "Thandi Nkosi",
            DonorEmail = "thandi@example.com",
            Amount = 250m
        };

        [Fact]
        public void Donation_WithValidDetails_PassesValidation()
        {
            Assert.Empty(Validate(ValidDonation()));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-50)]
        public void Donation_WithAmountBelowOne_FailsValidation(double amount)
        {
            var donation = ValidDonation();
            donation.Amount = (decimal)amount;

            Assert.Contains(Validate(donation), r => r.MemberNames.Contains(nameof(Donation.Amount)));
        }

        [Fact]
        public void Donation_WithInvalidEmail_FailsValidation()
        {
            var donation = ValidDonation();
            donation.DonorEmail = "not-an-email";

            Assert.Contains(Validate(donation), r => r.MemberNames.Contains(nameof(Donation.DonorEmail)));
        }

        [Fact]
        public void Donation_Defaults_AreZarAndOneTime()
        {
            var donation = new Donation();

            Assert.Equal("ZAR", donation.Currency);
            Assert.Equal("One-time", donation.DonationType);
        }

        [Fact]
        public void Volunteer_WithoutFullName_FailsValidation()
        {
            var volunteer = new Volunteer
            {
                UserId = "user-1",
                FullName = "",
                Email = "sipho@example.com",
                PhoneNumber = "0821234567"
            };

            Assert.Contains(Validate(volunteer), r => r.MemberNames.Contains(nameof(Volunteer.FullName)));
        }

        [Fact]
        public void Volunteer_WithValidDetails_PassesValidation()
        {
            var volunteer = new Volunteer
            {
                UserId = "user-1",
                FullName = "Sipho Dlamini",
                Email = "sipho@example.com",
                PhoneNumber = "0821234567",
                Skills = "First aid",
                Availability = "Weekends"
            };

            Assert.Empty(Validate(volunteer));
        }

        [Fact]
        public void AreaOfNeed_DefaultSeverity_IsMedium()
        {
            Assert.Equal("Medium", new AreaOfNeed().Severity);
        }
    }
}
