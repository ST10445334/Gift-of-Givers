using System.Diagnostics;
using Gift_of_Givers.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gift_of_Givers.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Home page
        public IActionResult Index()
        {
            return View();
        }

        // About page
        public IActionResult About()
        {
            return View();
        }

        // Keep Privacy for now
        public IActionResult Privacy()
        {
            return View();
        }

        // Error page
        [ResponseCache(
            Duration = 0,
            Location = ResponseCacheLocation.None,
            NoStore = true)]
        public IActionResult Error()
        {
            return View(
                new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id
                        ?? HttpContext.TraceIdentifier
                });
        }
    }
}