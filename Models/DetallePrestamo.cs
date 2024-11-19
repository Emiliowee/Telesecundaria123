using System.ComponentModel.DataAnnotations;

public class DetallePrestamo
{
    [Required]
    [Key]
    public int IDDePE { get; set; }
    [Required]
    public int IDPrestamo { get; set; }
    [Required]
    public int IDMaterial { get; set; }
    [Required]
    public int Cantidad { get; set; }

    // Relación
}