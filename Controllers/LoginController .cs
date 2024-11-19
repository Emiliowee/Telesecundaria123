using Microsoft.AspNetCore.Mvc;
using ProyTelesecundaria.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProyTelesecundaria.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<LoginController> _logger;

        public LoginController(ApplicationDbContext context, ILogger<LoginController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate(string correo, string contraseña)
        {
            _logger.LogInformation($"Intento de login para correo: {correo}");

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Correo == correo && u.Contraseña == contraseña);

            if (usuario != null)
            {
                _logger.LogInformation("Login exitoso");
                return Json(new { success = true, redirectUrl = "/Home/Principal" });
            }

            _logger.LogWarning("Login fallido");
            return Json(new { success = false, message = "Credenciales inválidas" });
        }
    }
}
