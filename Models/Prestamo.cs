using System.ComponentModel.DataAnnotations;

public class Prestamo
{
    [Required]
    [Key]
    public int? IDPrestamo { get; set; }
    [Required]
    public string? Fecha { get; set; }
    [Required]
    public int? Matricula { get; set; }

    // Relación
}