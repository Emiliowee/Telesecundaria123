using Microsoft.EntityFrameworkCore;
using ProyTelesecundaria.Models;

namespace ProyTelesecundaria
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Aula> Aula { get; set; }
        public DbSet<Maestros> Maestros { get; set; }
        public DbSet<Alumnos> Alumnos { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<AlumnoMateria> AlumnoMateria { get; set; }
        public DbSet<MaestroMateria> MaestroMateria { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de AlumnoMateria
            modelBuilder.Entity<AlumnoMateria>()
                .HasKey(am => new { am.Matricula, am.IDMateria, am.PeriodoBimestre });

            modelBuilder.Entity<AlumnoMateria>()
                .HasOne(am => am.Alumno)
                .WithMany(a => a.AlumnoMaterias)
                .HasForeignKey(am => am.Matricula)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AlumnoMateria>()
                .HasOne(am => am.Materia)
                .WithMany(m => m.AlumnoMaterias)
                .HasForeignKey(am => am.IDMateria)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de MaestroMateria
            modelBuilder.Entity<MaestroMateria>()
                .HasKey(mm => new { mm.IDMaestro, mm.IDMateria });

            modelBuilder.Entity<MaestroMateria>()
                .HasOne(mm => mm.Maestro)
                .WithMany(m => m.MaestroMaterias)
                .HasForeignKey(mm => mm.IDMaestro)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MaestroMateria>()
                .HasOne(mm => mm.Materia)
                .WithMany(m => m.MaestroMaterias)
                .HasForeignKey(mm => mm.IDMateria)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuración de nombres de tablas
            modelBuilder.Entity<Usuario>().ToTable("Usuarios");
            modelBuilder.Entity<Aula>().ToTable("Aulas");
            modelBuilder.Entity<Maestros>().ToTable("Maestros");
            modelBuilder.Entity<Alumnos>().ToTable("Alumnos");
            modelBuilder.Entity<Materia>().ToTable("Materias");
            modelBuilder.Entity<AlumnoMateria>().ToTable("AlumnoMateria");
            modelBuilder.Entity<MaestroMateria>().ToTable("MaestroMateria");
        }
    }
}
