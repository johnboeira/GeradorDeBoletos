namespace GeradorDeBoletos.Web.API.DTOs.Features.Bancos;

public class BancoDetalheDTO
{
    public int Id { get; set; }
    public string NomeBanco { get; set; }
    public string CodigoBanco { get; set; }
    public decimal PercentualDeJuros { get; set; }
}