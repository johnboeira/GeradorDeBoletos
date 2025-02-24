namespace GeradorDeBoletos.Domain.Features.Boletos;

public interface IBoletoRepository
{
    /// <summary>
    /// Cria boleto
    /// </summary>
    /// <param name="boleto"></param>
    /// <returns></returns>
    public Task CriaAsync(Boleto boleto);

    /// <summary>
    /// Busca todos os boletos asNoTracking
    /// </summary>
    /// <returns>Retorna todos boletos</returns>
    public Task<IEnumerable<Boleto>> BuscaTodos();

    /// <summary>
    /// Busca  boleto asNoTracking
    /// </summary>
    /// <param name="id">id para busca</param>
    /// <returns>Retorna um boleto</returns>
    public Task<Boleto> Busca(int id);
}
