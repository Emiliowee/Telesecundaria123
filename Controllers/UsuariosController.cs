using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyTelesecundaria.Models;
using System.Threading.Tasks;

namespace ProyTelesecundaria.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Usuarios
        public async Task<IActionResult> Index()
        {
            var usuarios = await _context.Usuarios.ToListAsync();
            return View(usuarios);
        }

        // GET: Usuarios/Crear
        public IActionResult Crear()
        {
            return View();
        }

        // POST: Usuarios/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([FromBody] Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(usuario);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear usuario: {ex.Message}");
            }
        }

        // GET: Usuarios/Editar/5
        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Editar/5
        [HttpPost]
        public async Task<IActionResult> Editar([FromBody] Usuario usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var usuarioExistente = await _context.Usuarios.FindAsync(usuario.IDUsuario);
                if (usuarioExistente == null)
                {
                    return NotFound();
                }

                usuarioExistente.Nombre = usuario.Nombre;
                usuarioExistente.Correo = usuario.Correo;
                usuarioExistente.Contraseña = usuario.Contraseña;
                usuarioExistente.TipoUsuario = usuario.TipoUsuario;

                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST: Usuarios/Eliminar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Usuarios/Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var usuario = await _context.Usuarios.FindAsync(id);
                if (usuario == null)
                {
                    return NotFound();
                }

                _context.Usuarios.Remove(usuario);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // GET: Usuarios/BuscarUsuario/5
        [HttpGet]
        public async Task<IActionResult> BuscarUsuario(int id)
        {
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            
            // Asegurarse de que todos los campos se incluyan en la respuesta
            var usuarioResponse = new
            {
                idUsuario = usuario.IDUsuario,
                nombre = usuario.Nombre,
                correo = usuario.Correo,
                contraseña = usuario.Contraseña,
                tipoUsuario = usuario.TipoUsuario
            };
            
            return Json(usuarioResponse);
        }

        private bool UsuarioExists(int id)
        {
            return _context.Usuarios.Any(e => e.IDUsuario == id);
        }

        public class IdRequest
        {
            public int Id { get; set; }
        }
    }
} 