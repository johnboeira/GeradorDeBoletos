using GeradorDeBoletos.Domain.Features.Bancos;
using GeradorDeBoletos.Infra.Data.Features.Bancos;

namespace GeradorDeBoletos.Services.Features.Bancos;

public class BancoService
{
    public BancoRepository _bancoRepository;

    public BancoService(BancoRepository bancoRepository)
    {
        _bancoRepository = bancoRepository;
    }

    public async Task CriarBancoAsync(Banco banco)
    {
        await _bancoRepository.CriaAsync(banco);
    }

    public async Task<IEnumerable<Banco>> BuscaTodos()
    {
        return await _bancoRepository.BuscaTodos();
    }

    public async Task<Banco> Busca(int id)
    {
        return await _bancoRepository.Busca(id);
    }
}