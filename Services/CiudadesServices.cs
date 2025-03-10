using Enmanuel_Gomez_P2_AP1.DAL;
using Enmanuel_Gomez_P2_AP1.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Enmanuel_Gomez_P2_AP1.Services;

public class ArticulosService(IDbContextFactory<Context> DbFactory)
{
    public async Task<List<Articulos>> Listar(Expression<Func<Articulos, bool>> criterio)
    {
        await using var contexto = await DbFactory.CreateDbContextAsync();

        return await contexto.Articulos
            .AsNoTracking()
            .Where(criterio)
            .ToListAsync();
    }
}
