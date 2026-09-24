using Gift_of_Givers.Data;
using Gift_of_Givers.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gift_of_Givers.Controllers
{
    public class DonationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public DonationController(ApplicationDbContext context,
                                  UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // PUBLIC info page
        [AllowAnonymous]
        public IActionResult Index() => View();

        // AUTHENTICATED form
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Donate()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(new Donation
            {
                DonorEmail = user?.Email ?? "",
                DonorName = user?.UserName ?? ""
            });
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Donate(Donation donation)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            donation.UserId = user.Id;
            ModelState.Remove(nameof(Donation.UserId));   // set server-side, not posted by the form
            if (!ModelState.IsValid) return View(donation);

            _context.Donations.Add(donation);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Thank you for your donation!";
            return RedirectToAction(nameof(Donate));
        }
    }
}