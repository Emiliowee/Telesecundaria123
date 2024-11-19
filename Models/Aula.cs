using System.ComponentModel.DataAnnotations;

public class Aula
{
    [Required]
    [Key]
    public int IDAula { get; set; }
    [Required]
    public int Capacidad { get; set; }
  
    // Relación
}