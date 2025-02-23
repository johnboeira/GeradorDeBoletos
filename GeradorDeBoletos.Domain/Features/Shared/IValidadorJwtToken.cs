namespace GeradorDeBoletos.Domain.Features.Shared;

public interface IValidadorJwtToken
{
    public int Validar(string token);
}
