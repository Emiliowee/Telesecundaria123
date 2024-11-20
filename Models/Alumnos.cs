using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyTelesecundaria.Models
{
    [Table("Alumnos")]
    public class Alumnos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Matricula { get; set; }

        [Required]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [StringLength(100)]
        public string Direccion { get; set; } = string.Empty;

        [Required]
        [StringLength(15)]
        public string Telefono { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string Correo { get; set; } = string.Empty;

        [Required]
        [Range(1, 6)]
        public int Grado { get; set; }

        [Required]
        [StringLength(1)]
        public string Grupo { get; set; } = string.Empty;

        [Required]
        [StringLength(100)]
        public string NombreTutor { get; set; } = string.Empty;

        [Required]
        [StringLength(15)]
        public string TelefonoTutor { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [StringLength(100)]
        public string CorreoTutor { get; set; } = string.Empty;

        // Propiedad de navegación para la relación con AlumnoMateria
        public virtual ICollection<AlumnoMateria> AlumnoMaterias { get; set; } = new List<AlumnoMateria>();
    }
} 