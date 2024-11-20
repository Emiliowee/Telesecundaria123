using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyTelesecundaria.Models
{
    [Table("Maestros")]
    public class Maestros
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IdMaestro { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Apellidos { get; set; } = string.Empty;

        [Required]
        [StringLength(15)]
        public string Telefono { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Correo { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Horario { get; set; } = string.Empty;

        [Required]
        [ForeignKey("Aula")]
        public int IdAula { get; set; }

        public virtual Aula? Aula { get; set; }

        // Propiedad de navegación para la relación con MaestroMateria
        public virtual ICollection<MaestroMateria> MaestroMaterias { get; set; } = new List<MaestroMateria>();
    }
}