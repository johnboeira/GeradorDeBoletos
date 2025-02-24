using GeradorDeBoletos.Domain.Features.Bancos;
using Microsoft.EntityFrameworkCore;

namespace GeradorDeBoletos.Infra.Data.Features.Bancos;

public class BancoRepository : IBancoRepository
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

    public async Task<bool> ExisteAsync(int id)
    {
        return await _dbContext.Bancos
            .AsNoTracking()
            .AnyAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<Banco>> BuscaTodosAsync()
    {
        return await _dbContext.Bancos
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Banco> BuscaAsync(int id)
    {
        return await _dbContext.Bancos
            .AsNoTracking()
            .SingleOrDefaultAsync(b => b.Id == id);
    }
}