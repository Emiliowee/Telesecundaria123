using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyTelesecundaria.Models;

namespace ProyTelesecundaria.Controllers
{
    public class MateriasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MateriasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Materias
        public async Task<IActionResult> Index()
        {
            try 
            {
                var materias = await _context.Materias
                    .OrderBy(m => m.IDMateria)
                    .ToListAsync();
                return View(materias);
            }
            catch (Exception ex)
            {
                // Log the error
                return View(new List<Materia>()); // Retorna una lista vacía en lugar de un error
            }
        }

        // GET: Materias/BuscarMateria/5
        [HttpGet]
        public async Task<IActionResult> BuscarMateria(int id)
        {
            var materia = await _context.Materias.FindAsync(id);
            if (materia == null)
            {
                return Json(new { success = false, message = "Materia no encontrada" });
            }

            return Json(new { 
                success = true,
                materia = new { 
                    idMateria = materia.IDMateria,
                    nombre = materia.Nombre
                }
            });
        }

        // POST: Materias/Crear
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([FromBody] Materia materia)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _context.Materias.AnyAsync(m => m.IDMateria == materia.IDMateria))
                    {
                        return Json(new { success = false, message = "El ID de la materia ya existe" });
                    }

                    if (await _context.Materias.AnyAsync(m => m.Nombre.ToLower() == materia.Nombre.ToLower()))
                    {
                        return Json(new { success = false, message = "Ya existe una materia con ese nombre" });
                    }

                    _context.Add(materia);
                    await _context.SaveChangesAsync();
                    return Json(new { success = true, message = "Materia registrada correctamente" });
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al crear materia: {ex.Message}");
            }
        }

        // POST: Materias/Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar([FromBody] Materia materia)
        {
            try
            {
                var materiaExistente = await _context.Materias.FindAsync(materia.IDMateria);
                if (materiaExistente == null)
                {
                    return NotFound("Materia no encontrada");
                }

                if (await _context.Materias.AnyAsync(m => 
                    m.Nombre.ToLower() == materia.Nombre.ToLower() && 
                    m.IDMateria != materia.IDMateria))
                {
                    return BadRequest("Ya existe otra materia con ese nombre");
                }

                materiaExistente.Nombre = materia.Nombre;

                await _context.SaveChangesAsync();
                return Ok("Materia modificada correctamente");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al modificar materia: {ex.Message}");
            }
        }

        // POST: Materias/Eliminar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar([FromBody] int id)
        {
            try
            {
                var materia = await _context.Materias.FindAsync(id);
                if (materia == null)
                {
                    return Json(new { success = false, message = "Materia no encontrada" });
                }

                // Verificar si la materia está siendo utilizada
                if (await _context.AlumnoMateria.AnyAsync(am => am.IDMateria == id))
                {
                    return Json(new { success = false, message = "No se puede eliminar la materia porque está asignada a uno o más alumnos" });
                }

                if (await _context.MaestroMateria.AnyAsync(mm => mm.IDMateria == id))
                {
                    return Json(new { success = false, message = "No se puede eliminar la materia porque está asignada a uno o más maestros" });
                }

                _context.Materias.Remove(materia);
                await _context.SaveChangesAsync();
                return Json(new { success = true, message = "Materia eliminada correctamente" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = $"Error al eliminar materia: {ex.Message}" });
            }
        }

        private bool MateriaExists(int id)
        {
            return _context.Materias.Any(e => e.IDMateria == id);
        }
    }
} 