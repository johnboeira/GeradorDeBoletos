namespace GeradorDeBoletos.Domain.Features.Usuarios;

public interface IUsuarioRepository
{
    /// <summary>
    /// Cria usuário
    /// </summary>
    /// <param name="usuario"></param>
    /// <returns></returns>
    public Task CriaAsync(Usuario usuario);

    /// <summary>
    /// Verifica se existe por email
    /// </summary>
    /// <param name="email"></param>
    /// <returns>True caso exista</returns>
    public Task<bool> ExistePorEmailAsync(string email);
    /// <summary>
    /// Verifica se existe por id
    /// </summary>
    /// <param name="id"></param>
    /// <returns>True caso exista</returns>
    public Task<bool> ExistePorIdAsync(int id);

    /// <summary>
    /// Valida um login
    /// </summary>
    /// <param name="email"></param>
    /// <param name="senha"></param>
    /// <returns>id do usuário caso ache, 0 caso não ache</returns>
    public Task<int> ExisteLoginAsync(string email, string senha);
}
