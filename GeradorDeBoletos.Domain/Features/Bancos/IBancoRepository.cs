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
    /// Verifica se existe por id
    /// </summary>
    /// <param name="id">id para validar se existe</param>
    /// <returns>True se existir</returns>
    public Task<bool> ExisteAsync(int id);

    /// <summary>
    /// Verifica se existe por codigo
    /// </summary>
    /// <param name="codigo"></param>
    /// <returns>True se existir</returns>
    public Task<bool> ExistePorCodigoAsync(string codigo);

    /// <summary>
    /// Busca usando asNoTracking
    /// </summary>
    /// <returns>Retorna todos os bancos</returns>
    public Task<IEnumerable<Banco>> BuscaTodosAsync();

    /// <summary>
    /// Busca usando asNoTracking
    /// </summary>
    /// <param name="codigo"></param>
    /// <returns>Retorna o banco</returns>
    public Task<Banco> BuscaPorCodigoAsync(string codigo);
}
