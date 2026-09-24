using Gift_of_Givers.Data;
using Gift_of_Givers.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gift_of_Givers.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.TotalDonations = await _context.Donations
                .SumAsync(d => (decimal?)d.Amount) ?? 0m;
            ViewBag.DonationCount = await _context.Donations.CountAsync();
            ViewBag.VolunteerCount = await _context.Volunteers.CountAsync();
            ViewBag.UserCount = await _userManager.Users.CountAsync();

            return View();
        }

        public async Task<IActionResult> Volunteers()
        {
            var volunteers = await _context.Volunteers
                .Include(v => v.User)
                .OrderByDescending(v => v.RegisteredAt)
                .ToListAsync();

            return View(volunteers);
        }

        public async Task<IActionResult> Donors()
        {
            var donations = await _context.Donations
                .Include(d => d.User)
                .OrderByDescending(d => d.DonatedAt)
                .ToListAsync();

            return View(donations);
        }

        public IActionResult Users()
        {
            var users = _userManager.Users
                .OrderBy(u => u.Email)
                .ToList();

            return View(users);
        }
    }
}