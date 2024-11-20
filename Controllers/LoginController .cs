using Microsoft.AspNetCore.Mvc;
using ProyTelesecundaria.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ProyTelesecundaria.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LoginController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserLoggedIn") == "true")
            {
                return RedirectToAction("Principal", "Home");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string correo, string contraseña)
        {
            try
            {
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Correo == correo && u.Contraseña == contraseña);

                if (usuario != null)
                {
                    HttpContext.Session.SetString("UserLoggedIn", "true");
                    HttpContext.Session.SetString("UserName", usuario.Nombre);
                    HttpContext.Session.SetString("UserType", usuario.TipoUsuario);
                    return RedirectToAction("Principal", "Home");
                }

                TempData["LoginError"] = "Usuario o contraseña incorrectos";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["LoginError"] = "Error al intentar iniciar sesión";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}
