using GeradorDeBoletos.Domain.Features.Bancos;
using GeradorDeBoletos.Domain.Features.Boletos;
using GeradorDeBoletos.Domain.Features.Shared.Exceptions;
using Microsoft.Extensions.Logging;

namespace GeradorDeBoletos.Services.Features.Boletos;

public class BoletoService
{
    public readonly IBoletoRepository _boletoRepository;
    public readonly IBancoRepository _bancoRepository;
    private ILogger<BoletoService> _logger;

    public BoletoService(IBoletoRepository boletoRepository, IBancoRepository bancoRepository, ILogger<BoletoService> logger)
    {
        _boletoRepository = boletoRepository;
        _bancoRepository = bancoRepository;
        _logger = logger;
    }

    public async Task CriarBoletoAsync(Boleto boleto)
    {
        var bancoExiste = await _bancoRepository.ExisteAsync(boleto.BancoId);

        if (bancoExiste is false)
        {
            var exception = new NotFoundException($"Não foi encontrado banco com id: {boleto.BancoId}");
            _logger.LogError(exception.Message);
            throw exception;
        }

        await _boletoRepository.CriaAsync(boleto);
    }

    public async Task<IEnumerable<Boleto>> BuscaTodos()
    {
        return await _boletoRepository.BuscaTodos();
    }

    public async Task<Boleto> Busca(int id)
    {
        return await _boletoRepository.Busca(id);
    }
}