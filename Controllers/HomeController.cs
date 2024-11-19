using Microsoft.AspNetCore.Mvc;

namespace ProyTelesecundaria.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Principal()
        {
            return View();
        }
    }
} 