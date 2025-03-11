using EnmanuelGomez_AP1_P2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System.Linq.Expressions;

namespace Enmanuel_Gomez_P2_AP1.Services;

public class EncuestasService(IDbContextFactory<Context> DbFactory)
{

    private async Task<bool> Existe(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Encuestas
            .AnyAsync(c => c.EncuestaId == id);
    }

    private async Task<bool> Insertar(Encuestas modelo)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        await AfectarMonto(modelo.Detalle.ToArray(), true);
        contexto.Encuestas.Add(modelo);
        return await contexto.SaveChangesAsync() > 0;
    }

    private async Task<bool> Modificar(Encuestas modelo)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var modeloOriginal = await contexto.Encuestas
            .Include(t => t.Detalle)
            .FirstOrDefaultAsync(t => t.EncuestaId == modelo.EncuestaId);

        if (modeloOriginal == null)
            return false;

        await AfectarMonto(modeloOriginal.Detalle.ToArray(), false);

        foreach (var detalleOriginal in modeloOriginal.Detalle)
        {
            if (!modelo.Detalle.Any(d => d.DetalleId == detalleOriginal.DetalleId))
            {
                contexto.EncuestaDetalle.Remove(detalleOriginal);
            }
        }

        await AfectarMonto(modelo.Detalle.ToArray(), true);

        contexto.Entry(modeloOriginal).CurrentValues.SetValues(modelo);

        foreach (var detalle in modelo.Detalle)
        {
            var detalleExistente = modeloOriginal.Detalle
                .FirstOrDefault(d => d.DetalleId == detalle.DetalleId);

            if (detalleExistente != null) contexto.Entry(detalleExistente).CurrentValues.SetValues(detalle);
            else modeloOriginal.Detalle.Add(detalle);
        }

        return await contexto.SaveChangesAsync() > 0;
    }
    public async Task<bool> Guardar(Encuestas modelo)
    {
        if (!await Existe(modelo.EncuestaId))
            return await Insertar(modelo);
        else
            return await Modificar(modelo);
    }
    public async Task<Encuestas?> Buscar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Encuestas
            .Include(m => m.Detalle)
            .FirstOrDefaultAsync(c => c.EncuestaId == id);
    }
    
    public async Task<bool> Eliminar(int id)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        var modelo = await contexto.Encuestas
            .Include(t => t.Detalle)
            .FirstOrDefaultAsync(t => t.EncuestaId == id);

        if (modelo == null)
            return false;

        await AfectarMonto(modelo.Detalle.ToArray(), resta: false);

        contexto.EncuestaDetalle.RemoveRange(modelo.Detalle);
        contexto.Encuestas.Remove(modelo);

        return await contexto.SaveChangesAsync() > 0;
    }

    public async Task<List<Encuestas>> Listar(Expression<Func<Encuestas, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Encuestas
            .Include(m => m.Detalle)
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }

    public async Task<bool> ExisteNombre(int modeloId, string? name)
    {
        await using var _contexto = await DbFactory.CreateDbContextAsync();

        return await _contexto.Encuestas
            .AnyAsync(e => e.EncuestaId != modeloId
            && e.Asignatura.ToLower().Equals(name.ToLower()));
    }

    public async Task AfectarMonto(EncuestaDetalle[] detalle, bool resta = true)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        foreach (var item in detalle)
        {
            var Ciudad = await contexto.Ciudades.SingleAsync(p => p.CiudadId == item.CiudadId);
            if (resta)
                Ciudad.Monto -= item.Monto;
            else
                Ciudad.Monto += item.Monto;
        }
        await contexto.SaveChangesAsync();
    }

}