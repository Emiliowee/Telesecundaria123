using System.ComponentModel.DataAnnotations;
namespace ProyTelesecundaria.Models
{
    public class Usuario
    {
        [Key]
        public int IDUsuario { get; set; }
        [Required]
        public string? Nombre { get; set; }
        [Required]
        [EmailAddress]
        public string? Correo { get; set; }
        [Required]
        public string? Contraseña { get; set; }
        [Required]
        public string? TipoUsuario { get; set; }
    }
}