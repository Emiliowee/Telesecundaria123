using Microsoft.EntityFrameworkCore;
using ProyTelesecundaria.Models;
using System.ComponentModel.DataAnnotations;

namespace ProyTelesecundaria
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public required DbSet<Usuario> Usuarios { get; set; }
        public required DbSet<Alumnos> Alumnos { get; set; }
        public required DbSet<Aula> Aula { get; set; }
        public required DbSet<MaestroMateria> MaestroMateria { get; set; }
        public required DbSet<Maestros> Maestros { get; set; }
        public required DbSet<DetallePrestamo> DetallePrestamo { get; set; }
        public required DbSet<Materiales> Materiales { get; set; }
        public required DbSet<Materias> Materias { get; set; }
        public required DbSet<Prestamo> Prestamo { get; set; }
    }
}
