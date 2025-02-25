using GeradorDeBoletos.Domain.Features.GeradorDeToken;
using GeradorDeBoletos.Domain.Features.Shared;
using GeradorDeBoletos.Domain.Features.Shared.Exceptions;
using GeradorDeBoletos.Domain.Features.Usuarios;
using GeradorDeBoletos.Infra.Criptografia;
using Microsoft.Extensions.Logging;

namespace GeradorDeBoletos.Services.Features.GeradorDeToken;

public class GeradorDeTokenService
{
    private readonly SenhaEncriptador _senhaEncriptador;
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IGeradorDeTokenDeAcesso _geradorJwtToken;
    private readonly ILogger<GeradorDeTokenService> _logger;

    public GeradorDeTokenService(SenhaEncriptador senhaEncriptador, IUsuarioRepository usuarioRepository, IGeradorDeTokenDeAcesso geradorJwtToken, ILogger<GeradorDeTokenService> logger)
    {
        _senhaEncriptador = senhaEncriptador;
        _usuarioRepository = usuarioRepository;
        _geradorJwtToken = geradorJwtToken;
        _logger = logger;
    }

    public async Task<string> GerarTokenDeAcesso(Login login)
    {
        var senhaEncriptada = _senhaEncriptador.Encriptar(login.Senha);

        var idDoUsuario = await _usuarioRepository.ExisteLoginAsync(login.Email, senhaEncriptada);

        if(idDoUsuario == 0)
        {
            var exception = new InvalidLogin($"Senha ou email incorreto!");
            _logger.LogError(exception.Message);
            throw exception;
        }

        return _geradorJwtToken.Gerar(idDoUsuario);
    }
}
