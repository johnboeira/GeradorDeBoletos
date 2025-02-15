using Microsoft.EntityFrameworkCore;

namespace GeradorDeBoletos.Infra.Data;

public class GerardorDeBoletosDbContext : DbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GerardorDeBoletosDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}