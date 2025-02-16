using GeradorDeBoletos.Domain.Features.Bancos;
using GeradorDeBoletos.Domain.Features.Boletos;
using Microsoft.EntityFrameworkCore;

namespace GeradorDeBoletos.Infra.Data;

public class GerardorDeBoletosDbContext : DbContext
{
    public GerardorDeBoletosDbContext(DbContextOptions<GerardorDeBoletosDbContext> options) : base(options)
    {
    }

    public DbSet<Banco> Bancos { get; set; }
    public DbSet<Boleto> Boletos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(GerardorDeBoletosDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}