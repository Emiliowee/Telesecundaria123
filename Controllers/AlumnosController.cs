using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyTelesecundaria.Models;

namespace ProyTelesecundaria.Controllers
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear([FromBody] Alumnos alumno)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Verificar si la matrícula ya existe
                    if (await _context.Alumnos.AnyAsync(a => a.Matricula == alumno.Matricula))
                    {
                        return BadRequest("La matrícula ya existe");
                    }

                    _context.Add(alumno);
                    await _context.SaveChangesAsync();
                    return Ok("Alumno registrado con éxito");
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al registrar alumno: {ex.Message}");
            }
        }

        public class EditarAlumnoModel
        {
            public int MatriculaOriginal { get; set; }
            public Alumnos Alumno { get; set; }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar([FromBody] EditarAlumnoModel modelo)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                var alumnoExistente = await _context.Alumnos.FindAsync(modelo.MatriculaOriginal);
                if (alumnoExistente == null)
                {
                    return NotFound("Alumno no encontrado");
                }

                // Si la matrícula cambió
                if (modelo.MatriculaOriginal != modelo.Alumno.Matricula)
                {
                    // Verificar que la nueva matrícula no exista
                    if (await _context.Alumnos.AnyAsync(a => a.Matricula == modelo.Alumno.Matricula))
                    {
                        return BadRequest("La nueva matrícula ya existe");
                    }

                    // Eliminar el alumno existente
                    _context.Alumnos.Remove(alumnoExistente);
                    await _context.SaveChangesAsync();

                    // Crear nuevo alumno con la nueva matrícula
                    var nuevoAlumno = new Alumnos
                    {
                        Matricula = modelo.Alumno.Matricula,
                        Nombre = modelo.Alumno.Nombre,
                        Apellido = modelo.Alumno.Apellido,
                        FechaNacimiento = modelo.Alumno.FechaNacimiento,
                        Direccion = modelo.Alumno.Direccion,
                        Telefono = modelo.Alumno.Telefono,
                        Correo = modelo.Alumno.Correo,
                        Grado = modelo.Alumno.Grado,
                        Grupo = modelo.Alumno.Grupo,
                        NombreTutor = modelo.Alumno.NombreTutor,
                        TelefonoTutor = modelo.Alumno.TelefonoTutor,
                        CorreoTutor = modelo.Alumno.CorreoTutor
                    };

                    _context.Alumnos.Add(nuevoAlumno);
                }
                else
                {
                    // Si la matrícula no cambió, actualizar los demás campos
                    alumnoExistente.Nombre = modelo.Alumno.Nombre;
                    alumnoExistente.Apellido = modelo.Alumno.Apellido;
                    alumnoExistente.FechaNacimiento = modelo.Alumno.FechaNacimiento;
                    alumnoExistente.Direccion = modelo.Alumno.Direccion;
                    alumnoExistente.Telefono = modelo.Alumno.Telefono;
                    alumnoExistente.Correo = modelo.Alumno.Correo;
                    alumnoExistente.Grado = modelo.Alumno.Grado;
                    alumnoExistente.Grupo = modelo.Alumno.Grupo;
                    alumnoExistente.NombreTutor = modelo.Alumno.NombreTutor;
                    alumnoExistente.TelefonoTutor = modelo.Alumno.TelefonoTutor;
                    alumnoExistente.CorreoTutor = modelo.Alumno.CorreoTutor;
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return Ok("Cambios realizados con éxito");
            }
            catch (Exception ex)
            {
                await transaction.RollbackAsync();
                return BadRequest($"Error al modificar alumno: {ex.Message}");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar([FromBody] Dictionary<string, int> data)
        {
            try
            {
                int matricula = data["matricula"];
                var alumno = await _context.Alumnos.FindAsync(matricula);
                if (alumno == null)
                {
                    return NotFound("Alumno no encontrado");
                }

                _context.Alumnos.Remove(alumno);
                await _context.SaveChangesAsync();
                return Ok("Alumno eliminado con éxito");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al eliminar alumno: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<IActionResult> BuscarAlumno(int matricula)
        {
            var alumno = await _context.Alumnos.FindAsync(matricula);
            if (alumno == null)
            {
                return NotFound("Alumno no encontrado");
            }
            return Json(alumno);
        }
    }
} 