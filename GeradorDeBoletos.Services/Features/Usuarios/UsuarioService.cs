﻿using GeradorDeBoletos.Domain.Features.Shared.Exceptions;
using GeradorDeBoletos.Domain.Features.Usuarios;
using GeradorDeBoletos.Infra.Criptografia;
using Microsoft.Extensions.Logging;

namespace GeradorDeBoletos.Services.Features.Usuarios;

public class UsuarioService
{
    public IUsuarioRepository _usuarioRepository;
    private ILogger<UsuarioService> _logger;
    private SenhaEncriptador _senhaEncriptador;

    public UsuarioService(IUsuarioRepository usuarioRepository, ILogger<UsuarioService> logger, SenhaEncriptador senhaEncriptador)
    {
        _usuarioRepository = usuarioRepository;
        _logger = logger;
        _senhaEncriptador = senhaEncriptador;
    }

    public async Task<string> CriarUsuarioAsync(Usuario usuario)
    {
        var usuarioJaExiste = await _usuarioRepository.ExistePorEmailAsync(usuario.Email);
        if (usuarioJaExiste)
        {
            var exception = new AlreadyExistsException($"Já existe usuário com email: {usuario.Email}");
            _logger.LogError(exception.Message);
            throw exception;
        }

        var senhaEncriptada = _senhaEncriptador.Encriptar(usuario.Senha);

        usuario.DefinirSenha(senhaEncriptada);

        await _usuarioRepository.CriaAsync(usuario);

        return usuario.Email;
    }
}