namespace GeradorDeBoletos.Domain.Features.Bancos;

public interface IBancoRepository
{
    /// <summary>
    /// Criar banco
    /// </summary>
    /// <param name="banco">Banco</param>
    /// <returns></returns>
    public Task CriaAsync(Banco banco);

    /// <summary>
    /// Busca usando asNoTracking
    /// </summary>
    /// <param name="id">id para validar se existe</param>
    /// <returns>True se existir</returns>
    public Task<bool> ExisteAsync(int id);

    /// <summary>
    /// Busca usando asNoTracking
    /// </summary>
    /// <returns>Retorna todos os bancos</returns>
    public Task<IEnumerable<Banco>> BuscaTodosAsync();

    /// <summary>
    /// Busca usando asNoTracking
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Retorna o banco</returns>
    public Task<Banco> BuscaAsync(int id);
}
