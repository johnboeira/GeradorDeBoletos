namespace GeradorDeBoletos.Domain.Features.Shared;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }
}