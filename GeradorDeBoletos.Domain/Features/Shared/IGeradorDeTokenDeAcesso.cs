namespace GeradorDeBoletos.Domain.Features.Shared;

public interface IGeradorDeTokenDeAcesso
{
    public string Gerar(int id);
}
