using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyTelesecundaria.Models
{
    [Table("MaestroMateria")]
    public class MaestroMateria
    {
        [Required]
        public int IDMaestro { get; set; }

        [Required]
        public int IDMateria { get; set; }

        // Propiedades de navegación
        [ForeignKey("IDMaestro")]
        public virtual Maestros Maestro { get; set; } = null!;

        [ForeignKey("IDMateria")]
        public virtual Materia Materia { get; set; } = null!;
    }
}