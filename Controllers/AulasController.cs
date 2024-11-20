using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyTelesecundaria.Models;
using System.Threading.Tasks;

namespace ProyTelesecundaria.Controllers
{
    public class AulasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AulasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var aulas = await _context.Aula.ToListAsync();
            return View(aulas);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([FromBody] Aula aula)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(aula);
                    await _context.SaveChangesAsync();
                    return Ok();
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                var innerException = ex.InnerException?.Message ?? "No inner exception";
                return BadRequest($"Error al crear aula: {innerException}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar([FromBody] Aula aula)
        {
            try
            {
                var aulaExistente = await _context.Aula.FindAsync(aula.IDAula);
                if (aulaExistente == null)
                {
                    return NotFound();
                }

                aulaExistente.Capacidad = aula.Capacidad;
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var aula = await _context.Aula.FindAsync(id);
                if (aula == null)
                {
                    return NotFound();
                }

                _context.Aula.Remove(aula);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        public async Task<IActionResult> BuscarAula(int id)
        {
            var aula = await _context.Aula.FindAsync(id);
            if (aula == null)
            {
                return NotFound();
            }
            return Json(aula);
        }
    }
} 