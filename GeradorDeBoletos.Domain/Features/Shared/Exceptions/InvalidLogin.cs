namespace GeradorDeBoletos.Domain.Features.Shared.Exceptions;

public class InvalidLogin : Exception
{
    public InvalidLogin(string message) : base(message)
    {
        
    }
}
