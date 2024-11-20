using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyTelesecundaria.Models
{
    [Table("AlumnoMateria")]
    public class AlumnoMateria
    {
        [Required]
        public int Matricula { get; set; }

        [Required]
        public int IDMateria { get; set; }

        [Required]
        [Range(0, 100)]
        public int Calificacion { get; set; }

        [Required]
        [StringLength(20)]
        public string PeriodoBimestre { get; set; } = string.Empty;

        // Propiedades de navegación
        [ForeignKey("Matricula")]
        public virtual Alumnos Alumno { get; set; } = null!;

        [ForeignKey("IDMateria")]
        public virtual Materia Materia { get; set; } = null!;
    }
}