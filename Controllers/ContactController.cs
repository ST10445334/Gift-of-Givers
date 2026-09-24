using Microsoft.AspNetCore.Mvc;

namespace Gift_of_Givers.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}