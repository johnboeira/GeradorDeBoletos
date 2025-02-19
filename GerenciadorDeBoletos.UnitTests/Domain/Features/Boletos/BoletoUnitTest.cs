using GeradorDeBoletos.Domain.Features.Bancos;
using GeradorDeBoletos.Domain.Features.Boletos;

namespace GerenciadorDeBoletos.UnitTests.Domain.Features.Boletos;

public class BoletoUnitTest
{
    [Fact]
    public void CalcularAplicacaoDeJuros_Com_VencimentoNoFuturo_NaoDeveAplicarJuros()
    {
        // Arrange
        var banco = new Banco { PercentualDeJuros = 10 };
        var boleto = new Boleto
        {
            Banco = banco,
            DataVencimento = DateTime.Now.AddDays(1)
        };
        boleto.Valor = 100;

        // Act
        var valorComJuros = boleto.Valor;

        // Assert
        Assert.Equal(100, valorComJuros);
    }

    [Fact]
    public void CalcularAplicacaoDeJuros_Com_VencimentoHoje_NaoDeveAplicarJuros()
    {
        // Arrange
        var banco = new Banco { PercentualDeJuros = 10 };
        var boleto = new Boleto
        {
            Banco = banco,
            DataVencimento = DateTime.Now
        };
        boleto.Valor = 100;

        // Act
        var valorComJuros = boleto.Valor;

        // Assert
        Assert.Equal(100, valorComJuros);
    }

    [Fact]
    public void CalcularAplicacaoDeJuros_Sem_TerCarregadoOBancoAinda_NaoDeveAplicarJuros()
    {
        // Arrange
        var boleto = new Boleto
        {
            Banco = null,
            DataVencimento = DateTime.Now.AddDays(1)
        };
        boleto.Valor = 100m;

        // Act
        var valorSemJuros = boleto.Valor;

        // Assert
        Assert.Equal(100m, valorSemJuros);
    }

    [Fact]
    public void CalcularAplicacaoDeJuros_Com_BoletoAtrasado_DeveAplicarJuros()
    {
        // Arrange
        var banco = new Banco { PercentualDeJuros = 10 };
        var boleto = new Boleto
        {
            Banco = banco,
            DataVencimento = DateTime.Now.AddDays(-1)
        };
        boleto.Valor = 100m;

        // Act
        var valorSemJuros = boleto.Valor;

        // Assert
        Assert.Equal(110m, valorSemJuros);
    }
}