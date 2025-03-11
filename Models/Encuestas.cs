using System.ComponentModel.DataAnnotations;

namespace EnmanuelGomez_AP1_P2.Models;

public class Encuestas
{
    [Key]
    public int EncuestaId { get; set; }

    [Required(ErrorMessage = "Debe rellenar este campo")]
    public string? Asignatura { get; set; }

    public DateTime Fecha { get; set; } = DateTime.Now;

    public double Monto { get; set; }

    public ICollection<EncuestaDetalle> Detalle { get; set; } = new List<EncuestaDetalle>();
}