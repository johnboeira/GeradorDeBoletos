using GeradorDeBoletos.Domain.Features.Shared;

namespace GeradorDeBoletos.Domain.Features.Usuarios;

public class Usuario : Entidade
{
    public string Email { get; set; } = string.Empty;
    public string Senha { get; private set; } = string.Empty;

    public void DefinirSenha(string senha)
    {
        Senha = senha;
    }
}