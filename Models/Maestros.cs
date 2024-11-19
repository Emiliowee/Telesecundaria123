using System.ComponentModel.DataAnnotations;

public class Maestros
{
    [Required]
    [Key]
    public int IDMaestro { get; set; }
    [Required]
    public string? Nombre { get; set; }
    [Required]
    public string? Telefono { get; set; }
    [Required]
    public string? Apellidos { get; set; }
    [Required]
    public string? Correo { get; set; }
    [Required]
    public string? Horario { get; set; }
    [Required]
    public string? IDAula { get; set; }

    // Relación
}