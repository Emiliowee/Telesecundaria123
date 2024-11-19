using System.ComponentModel.DataAnnotations;

public class AlumnoMAteria
{
    [Required]
    [Key]
    public int Matricula { get; set; }
    [Required]
    public int Calificacion { get; set; }
    [Required]
    public int IDMateria { get; set; }
    [Required]
    public string? PeriodoBimestre { get; set; }

    // Relación
}