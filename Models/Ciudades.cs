using System.ComponentModel.DataAnnotations;

namespace EnmanuelGomez_AP1_P2.Models;
public class Ciudades
{
    [Key]
    public int CiudadId { get; set; }
    public string? Nombre { get; set; }
    public double Monto { get; set; }
}
