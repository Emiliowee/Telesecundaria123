using System.ComponentModel.DataAnnotations;

public class MaestroMateria
{
    [Required]
    [Key]
    public int? IDMaestro { get; set; }
    [Required]
    public int? IDMateria { get; set; }
    
    // Relación
}