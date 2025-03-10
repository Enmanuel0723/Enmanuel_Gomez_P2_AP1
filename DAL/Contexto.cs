using Enmanuel_gomez_P2_AP1.Models
using Microsoft.EntityFrameworkCore;


public class Context(DbContextOptions<Context> options) : DbContext(options)
{
    public DbSet<Modelos> Modelos { get; set; }
    public DbSet<ModelosDetalle> ModelosDetalle { get; set; }
    public DbSet<Articulos> Articulos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Articulos>().HasData(new List<Articulos>()
        {
            new Articulos() {ArticuloId = 1, Nombre = "Campo1", Existencia = 50, Costo = 15, Precio = 30},
            new Articulos() {ArticuloId = 2, Nombre = "Campo2", Existencia = 40, Costo = 25, Precio = 60},
            new Articulos() {ArticuloId = 3, Nombre = "Campo3", Existencia = 30, Costo = 35, Precio = 70},
        });
    }
}
