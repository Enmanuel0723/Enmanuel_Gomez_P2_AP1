using EnmanuelGomez_AP1_P2.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Enmanuel_Gomez_P2_AP1.Services;

public class CiudadesService(IDbContextFactory<Context> DbFactory)
{
    public async Task<List<Ciudades>> Listar(Expression<Func<Ciudades, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Ciudades
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}
