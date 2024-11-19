using System.ComponentModel.DataAnnotations;

public class Materiales
{
    [Key]
    public int IDMaterial { get; set; }
    public string? Nombre { get; set; }
    public int? Cantidad { get; set; }
    public string? Descripcion { get; set; }

    // Relación
}