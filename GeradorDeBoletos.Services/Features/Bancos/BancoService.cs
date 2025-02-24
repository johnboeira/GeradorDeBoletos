using GeradorDeBoletos.Domain.Features.Bancos;
using GeradorDeBoletos.Domain.Features.Shared.Exceptions;
using Microsoft.Extensions.Logging;

namespace GeradorDeBoletos.Services.Features.Bancos;

public class BancoService
{
    public readonly IBancoRepository _bancoRepository;
    private ILogger<BancoService> _logger;

    public BancoService(IBancoRepository bancoRepository, ILogger<BancoService> logger)
    {
        _bancoRepository = bancoRepository;
        _logger = logger;
    }

    public async Task CriarBancoAsync(Banco banco)
    {
        var jaExisteComCodigo = await _bancoRepository.ExistePorCodigoAsync(banco.CodigoBanco);
        if (jaExisteComCodigo)
        {
            var exception = new AlreadyExistsException($"Já existe banco com código: {banco.CodigoBanco}");
            _logger.LogError(exception.Message);
            throw exception;
        }

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