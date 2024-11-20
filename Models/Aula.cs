using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyTelesecundaria.Models
{
    [Table("Aulas")]
    public class Aula
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int IDAula { get; set; }
        
        [Required]
        public int Capacidad { get; set; }
    }
}