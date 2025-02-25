using GeradorDeBoletos.Domain.Features.Shared.Exceptions;
using GeradorDeBoletos.Domain.Features.Usuarios;
using GeradorDeBoletos.Infra.Criptografia;
using GeradorDeBoletos.Services.Features.Usuarios;
using Microsoft.Extensions.Logging;
using Moq;

namespace GerenciadorDeBoletos.UnitTests.Service.Features.Usuarios;

public class UsuarioServiceUnitTests
{
    private readonly UsuarioService _usuarioService;
    private readonly Mock<IUsuarioRepository> _usuarioRepositoryMock;
    private readonly Mock<SenhaEncriptador> _senhaEncriptadorMock;
    private readonly Mock<ILogger<UsuarioService>> _loggerMock;

    public UsuarioServiceUnitTests()
    {
        _usuarioRepositoryMock = new Mock<IUsuarioRepository>();
        _senhaEncriptadorMock = new Mock<SenhaEncriptador>();
        _loggerMock = new Mock<ILogger<UsuarioService>>();

        _usuarioService = new UsuarioService(
            _usuarioRepositoryMock.Object,
            _loggerMock.Object,
            _senhaEncriptadorMock.Object
        );
    }

    [Fact]
    public async Task CriarUsuarioAsync_Com_UsuarioJaExistente_Deve_LancarAlreadyExistsException()
    {
        // Arrange
        var usuario = CriarUsuario();
        _usuarioRepositoryMock
            .Setup(repo => repo.ExistePorEmailAsync(usuario.Email))
            .ReturnsAsync(true);

        // Act
        var exception = await Record.ExceptionAsync(() => _usuarioService.CriarUsuarioAsync(usuario));

        // Assert
        Assert.IsType<AlreadyExistsException>(exception);
        Assert.Equal($"Já existe usuário com email: {usuario.Email}", exception.Message);

        _usuarioRepositoryMock.Verify(repo => repo.CriaAsync(It.IsAny<Usuario>()), Times.Never);
    }

    [Fact]
    public async Task CriarUsuarioAsync_Deve_SalvarUsuario()
    {
        // Arrange
        var usuario = CriarUsuario();
        _usuarioRepositoryMock
            .Setup(repo => repo.ExistePorEmailAsync(usuario.Email))
            .ReturnsAsync(false);

        var senhaEncriptada = "senhaEncriptada";
        _senhaEncriptadorMock
            .Setup(s => s.Encriptar(usuario.Senha))
            .Returns(senhaEncriptada);

        // Act
        var resultadoEmail = await _usuarioService.CriarUsuarioAsync(usuario);

        // Assert
        Assert.Equal(senhaEncriptada, usuario.Senha);
        Assert.Equal(usuario.Email, resultadoEmail);

        _usuarioRepositoryMock.Verify(repo => repo.CriaAsync(usuario), Times.Once);
    }
    private Usuario CriarUsuario(string email = "teste@exemplo.com", string senha = "senha123")
    {
        var usuario = new Usuario
        {
            Email = email,
        };

        usuario.DefinirSenha(senha);

        return usuario;
    }
}
