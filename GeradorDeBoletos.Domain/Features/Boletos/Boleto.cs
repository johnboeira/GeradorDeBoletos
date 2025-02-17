using GeradorDeBoletos.Domain.Features.Bancos;
using GeradorDeBoletos.Domain.Features.Shared;

namespace GeradorDeBoletos.Domain.Features.Boletos;

public class Boleto : Entidade
{
    public string NomePagador { get; set; }
    public string CPFCNPJPagador { get; set; }
    public string NomeBeneficiario { get; set; }
    public string CPFCNPJBeneficiario { get; set; }
    private decimal _valorSemJuros;

    public decimal Valor
    {
        get => AplicarJuros();
        set => _valorSemJuros = value;
    }

    public DateTime DataVencimento { get; set; }
    public string Observacao { get; set; }

    public int BancoId { get; set; }
    public Banco Banco { get; set; }

    private decimal AplicarJuros()
    {
        if (Banco != null && DateTime.Now.Date > DataVencimento.Date)
        {
            var valorParaMultiplicarJuros = Banco.PercentualDeJuros / 100;
            var valorAcrescimoDeJuros = _valorSemJuros * valorParaMultiplicarJuros;

            return _valorSemJuros + valorAcrescimoDeJuros;
        }

        return _valorSemJuros;
    }
}