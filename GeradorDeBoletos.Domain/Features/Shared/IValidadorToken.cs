namespace GeradorDeBoletos.Domain.Features.Shared;

public interface IValidadorToken
{
    public int Validar(string token);
}
