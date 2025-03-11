using EnmanuelGomez_AP1_P2.Models;
using Microsoft.EntityFrameworkCore;


public class Context(DbContextOptions<Context> options) : DbContext(options)
{
    public DbSet<Encuestas> Encuestas { get; set; }
    public DbSet<EncuestaDetalle> EncuestaDetalle { get; set; }
    public DbSet<Ciudades> Ciudades { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Ciudades>().HasData(new List<Ciudades>()
        {
            new Ciudades() {CiudadId = 1, Nombre = "San Francisco de Macoris", Monto = 150000},
            new Ciudades() {CiudadId = 2, Nombre = "Salcedo", Monto = 70000},
            new Ciudades() {CiudadId = 3, Nombre = "Tenares", Monto = 40500},
        });
    }
}
