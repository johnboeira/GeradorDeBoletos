using GeradorDeBoletos.Domain.Features.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace GeradorDeBoletos.Infra.Data.Features.Usuarios;

public class UsuarioRepository
{
    private GerardorDeBoletosDbContext _dbContext;

    public UsuarioRepository(GerardorDeBoletosDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task CriaAsync(Usuario usuario)
    {
        await _dbContext.Usuarios.AddAsync(usuario);

        await _dbContext.SaveChangesAsync();
    }

    public async Task<bool> ExistePorEmailAsync(string email)
    {
        return await _dbContext.Usuarios.AnyAsync(b => b.Email == email);
    }

    public async Task<bool> ExistePorIdAsync(int id)
    {
        return await _dbContext.Usuarios.AnyAsync(b => b.Id == id);
    }

    public async Task<int> ExisteLoginAsync(string email, string senha)
    {
        return await _dbContext.Usuarios.Where(u => u.Email == email && u.Senha == senha)    
            .Select(u => u.Id)
            .FirstOrDefaultAsync();
    }
}