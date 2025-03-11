using System.ComponentModel.DataAnnotations;

namespace EnmanuelGomez_AP1_P2.Models;

public class EncuestaDetalle
{
    [Key]
    public int DetalleId { get; set; }

    public int CiudadId { get; set; }
    public Ciudades? Ciudad { get; set; }


    [Required(ErrorMessage = "Debe introducir un monto")]
    public double Monto { get; set; }
}
