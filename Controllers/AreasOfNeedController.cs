using Gift_of_Givers.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Gift_of_Givers.Controllers
{
    public class AreasOfNeedController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AreasOfNeedController(ApplicationDbContext context) => _context = context;

        [AllowAnonymous]
        public IActionResult Index() => View(_context.AreasOfNeed.ToList());
    }
}