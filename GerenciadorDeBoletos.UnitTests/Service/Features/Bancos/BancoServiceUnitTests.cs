using GeradorDeBoletos.Domain.Features.Bancos;
using GeradorDeBoletos.Domain.Features.Shared.Exceptions;
using GeradorDeBoletos.Services.Features.Bancos;
using Microsoft.Extensions.Logging;
using Moq;

namespace GerenciadorDeBoletos.UnitTests.Service.Features.Bancos;

public class BancoServiceUnitTests
{
    private readonly BancoService _bancoService;
    private readonly Mock<IBancoRepository> _bancoRepositoryMock;
    private readonly Mock<ILogger<BancoService>> _loggerMock;

    public BancoServiceUnitTests()
    {
        _bancoRepositoryMock = new Mock<IBancoRepository>();
        _loggerMock = new Mock<ILogger<BancoService>>();
        _bancoService = new BancoService(_bancoRepositoryMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task CriarBancoAsync_Com_BancoJaExistente_Deve_LancarException()
    {
        // Arrange
        var banco = new Banco { CodigoBanco = "001" };
        _bancoRepositoryMock
            .Setup(r => r.ExistePorCodigoAsync(banco.CodigoBanco))
            .ReturnsAsync(true);

        // Act 
        var exception = await Record.ExceptionAsync(() => _bancoService.CriarBancoAsync(banco));

        //Assert
        Assert.IsType<AlreadyExistsException>(exception);
        Assert.Equal($"Já existe banco com código: {banco.CodigoBanco}", exception.Message);

        _bancoRepositoryMock.Verify(r => r.CriaAsync(It.IsAny<Banco>()), Times.Never);
    }

    [Fact]
    public async Task CriarBancoAsync_Deve_SalvarNoBanco()
    {
        // Arrange
        var banco = new Banco { CodigoBanco = "001" };
        _bancoRepositoryMock.Setup(r => r.ExistePorCodigoAsync(banco.CodigoBanco)).ReturnsAsync(false);

        // Act
        await _bancoService.CriarBancoAsync(banco);

        // Assert
        _bancoRepositoryMock.Verify(r => r.CriaAsync(banco), Times.Once);
    }

    [Fact]
    public async Task BuscaPorCodigoAsync_BancoNaoEncontrado_Deve_LancarNotFoundException()
    {
        // Arrange
        var codigo = "001";
        _bancoRepositoryMock.Setup(r => r.BuscaPorCodigoAsync(codigo))
            .ReturnsAsync((Banco)null);

        // Act
        var exception = await Record.ExceptionAsync(() => _bancoService.BuscaPorCodigoAsync(codigo));

        // Assert
        var notFoundException = Assert.IsType<NotFoundException>(exception);
        Assert.Equal($"Banco não encontrado: {codigo}", notFoundException.Message);
    }

    [Fact]
    public async Task BuscaPorCodigoAsync_BancoEncontrado_Deve_RetornarBanco()
    {
        // Arrange
        var codigo = "001";
        var bancoEsperado = new Banco { CodigoBanco = codigo };
        _bancoRepositoryMock
            .Setup(r => r.BuscaPorCodigoAsync(codigo))
            .ReturnsAsync(bancoEsperado);

        // Act
        var bancoRetornado = await _bancoService.BuscaPorCodigoAsync(codigo);

        // Assert
        Assert.NotNull(bancoRetornado);
        Assert.Equal(bancoEsperado, bancoRetornado);
    }
}
