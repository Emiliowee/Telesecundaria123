using Microsoft.AspNetCore.Mvc;
using ProyTelesecundaria.Models;
using Microsoft.EntityFrameworkCore;

namespace ProyTelesecundaria
{
    public class AlumnosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlumnosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var alumnos = await _context.Alumnos.ToListAsync();
            return View(alumnos);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Alumnos alumno)
        {
            if (ModelState.IsValid)
            {
                _context.Add(alumno);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(alumno);
        }

        public async Task<IActionResult> Editar(int matricula)
        {
            if (matricula == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos.FirstOrDefaultAsync(a => a.Matricula == matricula);
            if (alumno == null)
            {
                return NotFound();
            }
            return View(alumno);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(int Matricula, Alumnos alumno)
        {
            if (Matricula != alumno.Matricula)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alumno);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlumnoExists(alumno.Matricula))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(alumno);
        }

        public async Task<IActionResult> Eliminar(int matricula)
        {
            if (matricula == null)
            {
                return NotFound();
            }

            var alumno = await _context.Alumnos
                .FirstOrDefaultAsync(m => m.Matricula == matricula);
            if (alumno == null)
            {
                return NotFound();
            }

            return View(alumno);
        }

        // Eliminar un alumno (POST)
        [HttpPost, ActionName("Eliminar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EliminarConfirmado(int matricula)
        {
            var alumno = await _context.Alumnos.FindAsync(matricula);
            if (alumno != null)
            {
                _context.Alumnos.Remove(alumno);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
        private bool AlumnoExists(int matricula)
        {
            return _context.Alumnos.Any(e => e.Matricula == matricula);
        }
    }
}
