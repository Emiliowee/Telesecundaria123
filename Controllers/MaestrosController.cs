using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyTelesecundaria.Models;

namespace ProyTelesecundaria.Controllers
{
    public class MaestrosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MaestrosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Aulas = await _context.Aula.ToListAsync();
            var maestros = await _context.Maestros.ToListAsync();
            return View(maestros);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([FromBody] Maestros maestro)
        {
            try
            {
                ModelState.Remove("Aula");
                
                if (ModelState.IsValid)
                {
                    // Verificar si el ID ya existe
                    if (await _context.Maestros.AnyAsync(m => m.IdMaestro == maestro.IdMaestro))
                    {
                        return BadRequest("El ID del maestro ya existe");
                    }

                    // Verificar si el aula existe
                    if (!await _context.Aula.AnyAsync(a => a.IDAula == maestro.IdAula))
                    {
                        return BadRequest("El aula especificada no existe");
                    }

                    _context.Add(maestro);
                    await _context.SaveChangesAsync();
                    return Ok("Maestro registrado correctamente");
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear maestro: {ex.Message}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar([FromBody] Maestros maestro)
        {
            try
            {
                var maestroExistente = await _context.Maestros.FindAsync(maestro.IdMaestro);
                if (maestroExistente == null)
                {
                    return NotFound("Maestro no encontrado");
                }

                // Verificar si el aula existe
                if (!await _context.Aula.AnyAsync(a => a.IDAula == maestro.IdAula))
                {
                    return BadRequest("El aula especificada no existe");
                }

                maestroExistente.Nombre = maestro.Nombre;
                maestroExistente.Apellidos = maestro.Apellidos;
                maestroExistente.Telefono = maestro.Telefono;
                maestroExistente.Correo = maestro.Correo;
                maestroExistente.Horario = maestro.Horario;
                maestroExistente.IdAula = maestro.IdAula;

                await _context.SaveChangesAsync();
                return Ok("Maestro modificado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al modificar maestro: {ex.Message}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            try
            {
                var maestro = await _context.Maestros.FindAsync(id);
                if (maestro == null)
                {
                    return NotFound("Maestro no encontrado");
                }

                _context.Maestros.Remove(maestro);
                await _context.SaveChangesAsync();
                return Ok("Maestro eliminado correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar maestro: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> BuscarMaestro(int id)
        {
            var maestro = await _context.Maestros.FindAsync(id);
            if (maestro == null)
            {
                return NotFound("Maestro no encontrado");
            }
            return Json(maestro);
        }
    }
} 