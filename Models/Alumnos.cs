using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProyTelesecundaria.Models
{
   
    public class Alumnos
    {
        [Key]
        public int Matricula { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(50, ErrorMessage = "El nombre no puede tener más de 50 caracteres")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio")]
        [StringLength(50, ErrorMessage = "El apellido no puede tener más de 50 caracteres")]
        public string? Apellido { get; set; }

        [Required(ErrorMessage = "La fecha de nacimiento es obligatoria")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime FechaNacimiento { get; set; }

        [Required(ErrorMessage = "La dirección es obligatoria")]
        [StringLength(100)]
        public string? Direccion { get; set; }

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [Phone(ErrorMessage = "El número de teléfono no es válido")]
        public string? Telefono { get; set; }

        [Required(ErrorMessage = "El teléfono del tutor es obligatorio")]
        [Phone(ErrorMessage = "El número de teléfono del tutor no es válido")]
        public string? TelefonoTutor { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo no es válido")]
        public string? Correo { get; set; }

        [Required(ErrorMessage = "El grado es obligatorio")]
        public int? Grado { get; set; }

        [Required(ErrorMessage = "El grupo es obligatorio")]
        [StringLength(5, ErrorMessage = "El grupo no puede tener más de 5 caracteres")]
        public string? Grupo { get; set; }

        [Required(ErrorMessage = "El nombre del tutor es obligatorio")]
        [StringLength(50)]
        public string? NombreTutor { get; set; }

        [Required(ErrorMessage = "El correo del tutor es obligatorio")]
        [EmailAddress(ErrorMessage = "El correo del tutor no es válido")]
        public string? CorreoTutor { get; set; }
    }
}


