using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyTelesecundaria.Models
{
    [Table("Materias")]
    public class Materia
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDMateria { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;

        // Propiedades de navegación
        public virtual ICollection<AlumnoMateria> AlumnoMaterias { get; set; } = new List<AlumnoMateria>();
        public virtual ICollection<MaestroMateria> MaestroMaterias { get; set; } = new List<MaestroMateria>();
    }
} 