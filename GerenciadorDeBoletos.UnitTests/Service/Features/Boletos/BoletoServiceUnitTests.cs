using GeradorDeBoletos.Domain.Features.Bancos;
using GeradorDeBoletos.Domain.Features.Boletos;
using GeradorDeBoletos.Domain.Features.Shared.Exceptions;
using GeradorDeBoletos.Services.Features.Boletos;
using Microsoft.Extensions.Logging;
using Moq;

namespace GerenciadorDeBoletos.UnitTests.Service.Features.Boletos;

public class BoletoServiceUnitTests
{
    private readonly BoletoService _boletoService;
    private readonly Mock<IBancoRepository> _bancoRepositoryMock;
    private readonly Mock<IBoletoRepository> _boletoRepositoryMock;
    private readonly Mock<ILogger<BoletoService>> _loggerMock;

    public BoletoServiceUnitTests()
    {
        _bancoRepositoryMock = new Mock<IBancoRepository>();
        _boletoRepositoryMock = new Mock<IBoletoRepository>();
        _loggerMock = new Mock<ILogger<BoletoService>>();

        _boletoService = new BoletoService(
            _boletoRepositoryMock.Object,
            _bancoRepositoryMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task CriarBoletoAsync_Com_BancoInexistente_Deve_LancarNotFoundException()
    {
        // Arrange
        var boleto = CriarBoleto();
        _bancoRepositoryMock
            .Setup(repo => repo.ExisteAsync(boleto.BancoId))
            .ReturnsAsync(false);

        // Act
        var exception = await Record.ExceptionAsync(() => _boletoService.CriarBoletoAsync(boleto));

        // Assert
        Assert.NotNull(exception);
        Assert.IsType<NotFoundException>(exception);
        Assert.Equal($"Não foi encontrado banco com id: {boleto.BancoId}", exception.Message);

        _boletoRepositoryMock.Verify(repo => repo.CriaAsync(It.IsAny<Boleto>()), Times.Never);
    }

    [Fact]
    public async Task CriarBoletoAsync_Deve_SalvarNoBanco()
    {
        // Arrange
        var boleto = CriarBoleto();
        _bancoRepositoryMock
            .Setup(repo => repo.ExisteAsync(boleto.BancoId))
            .ReturnsAsync(true);

        // Act
        await _boletoService.CriarBoletoAsync(boleto);

        // Assert
        _boletoRepositoryMock.Verify(repo => repo.CriaAsync(boleto), Times.Once);
    }

    [Fact]
    public async Task Busca_Com_BoletoNaoEncontrado_Deve_LancarNotFoundException()
    {
        // Arrange
        int id = 1;
        _boletoRepositoryMock
            .Setup(repo => repo.BuscaAsync(id))
            .ReturnsAsync((Boleto)null);

        // Act
        var exception = await Record.ExceptionAsync(() => _boletoService.Busca(id));

        // Assert
        var notFoundException = Assert.IsType<NotFoundException>(exception);
        Assert.Equal($"Não foi encontrado boleto com id: {id}", notFoundException.Message);
    }

    [Fact]
    public async Task Busca_Deve_RetornarBoleto()
    {
        // Arrange
        int id = 1;
        var boletoEsperado = new Boleto { Id = id, BancoId = 1 };
        _boletoRepositoryMock
            .Setup(repo => repo.BuscaAsync(id))
            .ReturnsAsync(boletoEsperado);

        // Act
        var boletoRetornado = await _boletoService.Busca(id);

        // Assert
        Assert.NotNull(boletoRetornado);
        Assert.Equal(boletoEsperado, boletoRetornado);
    }

    private Boleto CriarBoleto(int bancoId = 1)
    {
        return new Boleto
        {
            BancoId = bancoId
        };
    }
}
