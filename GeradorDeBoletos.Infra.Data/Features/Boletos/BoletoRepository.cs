using GeradorDeBoletos.Domain.Features.Boletos;
using Microsoft.EntityFrameworkCore;

namespace GeradorDeBoletos.Infra.Data.Features.Boletos;

public class BoletoRepository
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

    public async Task<IEnumerable<Boleto>> BuscaTodos()
    {
        return await _dbContext.Boletos
            .Include(b => b.Banco)
            .ToListAsync();
    }

    public async Task<Boleto> Busca(int id)
    {
        return await _dbContext.Boletos
            .Include(b => b.Banco)
            .SingleOrDefaultAsync(b => b.Id == id);
    }
}