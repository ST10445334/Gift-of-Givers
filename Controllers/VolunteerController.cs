using Gift_of_Givers.Data;
using Gift_of_Givers.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Gift_of_Givers.Controllers
{
    public class VolunteerController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public VolunteerController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Requires sign-in
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            // Pre-fill from account
            var model = new Volunteer
            {
                Email = user?.Email ?? string.Empty,
                FullName = user?.UserName ?? string.Empty
            };

            // If they already submitted, show their existing record
            var existing = _context.Volunteers
                .FirstOrDefault(v => v.UserId == user!.Id);

            if (existing != null)
                model = existing;

            return View(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(Volunteer volunteer)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return Challenge();

            volunteer.UserId = user.Id;
            ModelState.Remove(nameof(Volunteer.UserId));   // set server-side, not posted by the form

            if (!ModelState.IsValid)
                return View(volunteer);

            // Update if already exists, otherwise insert
            var existing = _context.Volunteers
                .FirstOrDefault(v => v.UserId == user.Id);

            if (existing != null)
            {
                existing.FullName = volunteer.FullName;
                existing.Email = volunteer.Email;
                existing.PhoneNumber = volunteer.PhoneNumber;
                existing.Skills = volunteer.Skills;
                existing.Availability = volunteer.Availability;
            }
            else
            {
                _context.Volunteers.Add(volunteer);
            }

            await _context.SaveChangesAsync();

            TempData["Success"] = "Your volunteer details have been saved.";
            return RedirectToAction(nameof(Index));
        }
    }
}