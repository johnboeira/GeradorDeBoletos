using GeradorDeBoletos.Domain.Features.Shared;
using GeradorDeBoletos.Domain.Features.Usuarios;
using GeradorDeBoletos.Infra.Criptografia;
using GeradorDeBoletos.Infra.Data.Features.Usuarios;
using Microsoft.Extensions.Logging;

namespace GeradorDeBoletos.Services.Features.Usuarios;

public class UsuarioService
{
    public UsuarioRepository _usuarioRepository;
    private ILogger<UsuarioService> _logger;
    private SenhaEncriptador _senhaEncriptador;

    public UsuarioService(UsuarioRepository usuarioRepository, ILogger<UsuarioService> logger, SenhaEncriptador senhaEncriptador)
    {
        _usuarioRepository = usuarioRepository;
        _logger = logger;
        _senhaEncriptador = senhaEncriptador;
    }

    public async Task<string> CriarUsuarioAsync(Usuario usuario)
    {
        var usuarioJaExiste = await _usuarioRepository.ExistePorEmailAsync(usuario.Email);

        if (usuarioJaExiste == true)
        {
            var exception = new NotFoundException($"Já existe usuário com email: {usuario.Email}");
            _logger.LogError(exception.Message);
            throw exception;
        }

        var senhaEncriptada = _senhaEncriptador.Encriptar(usuario.Senha);

        usuario.DefinirSenha(senhaEncriptada);

        await _usuarioRepository.CriaAsync(usuario);

        return usuario.Email;
    }
}