namespace GeradorDeBoletos.Domain.Features.Shared;

public interface IGeradorDeTokenDeAcesso
{
    /// <summary>
    /// Gera token jwt
    /// </summary>
    /// <param name="id">usa id do usuário para gerar token</param>
    /// <returns>Token como string</returns>
    public string Gerar(int id);
}
