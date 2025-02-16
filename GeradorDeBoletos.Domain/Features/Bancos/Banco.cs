using GeradorDeBoletos.Domain.Features.Boletos;
using GeradorDeBoletos.Domain.Features.Shared;

namespace GeradorDeBoletos.Domain.Features.Bancos;

public class Banco : Entidade
{
    public string NomeBanco { get; set; }
    public string CodigoBanco { get; set; }
    public decimal PercentualDeJuros { get; set; }
}