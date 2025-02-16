using GeradorDeBoletos.Domain.Features.Bancos;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

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

    public async Task<bool> ExisteAsync(int id)
    {
        return await _dbContext.Bancos.AnyAsync(b => b.Id == id);
    }

    public async Task<IEnumerable<Banco>> BuscaTodos()
    {
        return await _dbContext.Bancos.ToListAsync();
    }

    public async Task<Banco> Busca(int id)
    {
        return await _dbContext.Bancos.SingleOrDefaultAsync(b => b.Id == id);
    }
}