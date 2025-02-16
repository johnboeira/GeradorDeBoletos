namespace GeradorDeBoletos.Web.API.DTOs.Features.Bancos;

public class BancoResumoDTO
{
    public int Id { get; set; }
    public string NomeBanco { get; set; }
    public string CodigoBanco { get; set; }
    public decimal PercentualDeJuros { get; set; }
}