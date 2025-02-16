using GeradorDeBoletos.Domain.Features.Bancos;
using GeradorDeBoletos.Domain.Features.Shared;

namespace GeradorDeBoletos.Domain.Features.Boletos;

public class Boleto : Entidade
{
    public string NomePagador { get; set; }
    public string CPFCNPJPagador { get; set; }
    public string NomeBeneficiario { get; set; }
    public string CPFCNPJBeneficiario { get; set; }
    public decimal Valor { get; set; }
    public DateTime DataVencimento { get; set; }
    public string Observacao { get; set; }

    public int BancoId { get; set; }
    public Banco Banco { get; set; }
}