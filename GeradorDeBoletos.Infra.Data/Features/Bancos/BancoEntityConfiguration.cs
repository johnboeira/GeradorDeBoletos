using GeradorDeBoletos.Domain.Features.Bancos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeradorDeBoletos.Infra.Data.Features.Bancos;

public class BancoEntityConfiguration : IEntityTypeConfiguration<Banco>
{
    public void Configure(EntityTypeBuilder<Banco> builder)
    {
        builder.ToTable("bancos");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
               .ValueGeneratedOnAdd();

        builder.Property(b => b.NomeBanco)
               .IsRequired(true)
               .HasMaxLength(100)
               .HasColumnType("varchar(100)");

        builder.Property(b => b.CodigoBanco)
               .IsRequired(true)
               .HasMaxLength(20)
               .HasColumnType("varchar(20)");

        builder.Property(b => b.PercentualDeJuros)
               .IsRequired(true)
               .HasColumnType("numeric(18,2)");
    }
}