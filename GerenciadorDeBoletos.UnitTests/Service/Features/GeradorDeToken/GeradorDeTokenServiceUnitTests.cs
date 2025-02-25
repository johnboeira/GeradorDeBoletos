using GeradorDeBoletos.Domain.Features.GeradorDeToken;
using GeradorDeBoletos.Domain.Features.Shared;
using GeradorDeBoletos.Domain.Features.Shared.Exceptions;
using GeradorDeBoletos.Domain.Features.Usuarios;
using GeradorDeBoletos.Infra.Criptografia;
using GeradorDeBoletos.Services.Features.GeradorDeToken;
using Microsoft.Extensions.Logging;
using Moq;

namespace GerenciadorDeBoletos.UnitTests.Service.Features.GeradorDeToken;

public class GeradorDeTokenUnitTests
{
    private readonly GeradorDeTokenService _geradorTokenService;
    private readonly Mock<SenhaEncriptador> _senhaEncriptadorMock;
    private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
    private readonly Mock<IGeradorDeTokenDeAcesso> _geradorJwtTokenMock;
    private readonly Mock<ILogger<GeradorDeTokenService>> _loggerMock;

    public GeradorDeTokenUnitTests()
    {
        _senhaEncriptadorMock = new Mock<SenhaEncriptador>();
        _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
        _geradorJwtTokenMock = new Mock<IGeradorDeTokenDeAcesso>();
        _loggerMock = new Mock<ILogger<GeradorDeTokenService>>();

        _geradorTokenService = new GeradorDeTokenService(
            _senhaEncriptadorMock.Object,
            _usuarioRepositoryMock.Object,
            _geradorJwtTokenMock.Object,
            _loggerMock.Object
        );
    }

    [Fact]
    public async Task GerarTokenDeAcesso_Com_UsuarioInvalido_DeveLancarInvalidLogin()
    {
        // Arrange
        var login = CriarLogin();
        var senhaEncriptada = "senhaEncriptada";

        _senhaEncriptadorMock
            .Setup(s => s.Encriptar(login.Senha))
            .Returns(senhaEncriptada);

        _usuarioRepositoryMock
            .Setup(u => u.ExisteLoginAsync(login.Email, senhaEncriptada))
            .ReturnsAsync(0);

        // Act
        var exception = await Record.ExceptionAsync(() => _geradorTokenService.GerarTokenDeAcesso(login));

        // Assert
        var invalidLoginException = Assert.IsType<InvalidLogin>(exception);
        Assert.Equal("Senha ou email incorreto!", invalidLoginException.Message);

        _geradorJwtTokenMock.Verify(g => g.Gerar(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async Task GerarTokenDeAcesso_Com_LoginValido_DeveRetornarToken()
    {
        // Arrange
        var login = CriarLogin();
        var senhaEncriptada = "senhaEncriptada";
        var usuarioId = 123;
        var tokenEsperado = "tokenValido";

        _senhaEncriptadorMock
            .Setup(s => s.Encriptar(login.Senha))
            .Returns(senhaEncriptada);

        _usuarioRepositoryMock
            .Setup(u => u.ExisteLoginAsync(login.Email, senhaEncriptada))
            .ReturnsAsync(usuarioId);

        _geradorJwtTokenMock
            .Setup(g => g.Gerar(usuarioId))
            .Returns(tokenEsperado);

        // Act
        var token = await _geradorTokenService.GerarTokenDeAcesso(login);

        // Assert
        Assert.Equal(tokenEsperado, token);
        _geradorJwtTokenMock.Verify(g => g.Gerar(usuarioId), Times.Once);
    }

    private Login CriarLogin(string email = "teste@exemplo.com", string senha = "senha123")
    {
        return new Login
        {
            Email = email,
            Senha = senha
        };
    }
}
