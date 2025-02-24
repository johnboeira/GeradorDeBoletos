namespace GeradorDeBoletos.Domain.Features.Shared;

public interface IValidadorToken
{
    /// <summary>
    /// Valida token
    /// </summary>
    /// <param name="token">token para validação</param>
    /// <returns>Retorna id do usuário</returns>
    public int Validar(string token);
}
