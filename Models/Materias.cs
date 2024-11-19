using System.ComponentModel.DataAnnotations;

public class Materias
{
    [Required]
    [Key]
    public int IDMateria { get; set; }
    [Required]
    public string? Nombre { get; set; }

    // Relación
}