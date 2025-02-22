using GeradorDeBoletos.Domain.Features.Bancos;
using Microsoft.EntityFrameworkCore;

namespace GeradorDeBoletos.Infra.Data.Features.Bancos;

public class BancoRepository
{
    private GerardorDeBoletosDbContext _dbContext;

    public BancoRepository(GerardorDeBoletosDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CriaAsync(Banco banco)
    {
        await _dbContext.Bancos.AddAsync(banco);

        await _dbContext.SaveChangesAsync();
    }

    /// <summary>
    /// Busca usando asNoTracking
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<bool> ExisteAsync(int id)
    {
        return await _dbContext.Bancos
            .AsNoTracking()
            .AnyAsync(b => b.Id == id);
    }

    /// <summary>
    /// Busca usando asNoTracking
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Banco>> BuscaTodosAsync()
    {
        return await _dbContext.Bancos
            .AsNoTracking()
            .ToListAsync();
    }

    /// <summary>
    /// Busca usando asNoTracking
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Banco> BuscaAsync(int id)
    {
        return await _dbContext.Bancos
            .AsNoTracking()
            .SingleOrDefaultAsync(b => b.Id == id);
    }
}