using GeradorDeBoletos.Domain.Features.Boletos;
using Microsoft.EntityFrameworkCore;

namespace GeradorDeBoletos.Infra.Data.Features.Boletos;

public class BoletoRepository : IBoletoRepository
{
    private GerardorDeBoletosDbContext _dbContext;

    public BoletoRepository(GerardorDeBoletosDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CriaAsync(Boleto boleto)
    {
        await _dbContext.Boletos.AddAsync(boleto);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<IEnumerable<Boleto>> BuscaTodosAsync()
    {
        return await _dbContext.Boletos
            .AsNoTracking()
            .Include(b => b.Banco)
            .ToListAsync();
    }

    public async Task<Boleto> BuscaAsync(int id)
    {
        return await _dbContext.Boletos
             .AsNoTracking()
            .Include(b => b.Banco)
            .SingleOrDefaultAsync(b => b.Id == id);
    }
}