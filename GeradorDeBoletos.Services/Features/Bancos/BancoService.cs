using GeradorDeBoletos.Domain.Features.Bancos;
using GeradorDeBoletos.Infra.Data.Features.Bancos;

namespace GeradorDeBoletos.Services.Features.Bancos;

public class BancoService
{
    public readonly IBancoRepository _bancoRepository;

    public BancoService(IBancoRepository bancoRepository)
    {
        _bancoRepository = bancoRepository;
    }

    public async Task CriarBancoAsync(Banco banco)
    {
        await _bancoRepository.CriaAsync(banco);
    }

    public async Task<IEnumerable<Banco>> BuscaTodos()
    {
        return await _bancoRepository.BuscaTodosAsync();
    }

    public async Task<Banco> Busca(int id)
    {
        return await _bancoRepository.BuscaAsync(id);
    }
}