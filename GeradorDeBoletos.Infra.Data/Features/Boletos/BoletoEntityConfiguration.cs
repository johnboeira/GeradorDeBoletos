using GeradorDeBoletos.Domain.Features.Boletos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeradorDeBoletos.Infra.Data.Features.Boletos;

public class BoletoEntityConfiguration : IEntityTypeConfiguration<Boleto>
{
    public void Configure(EntityTypeBuilder<Boleto> builder)
    {
        builder.ToTable("boletos");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
               .ValueGeneratedOnAdd();

        builder.Property(b => b.NomePagador)
               .IsRequired(true)
               .HasMaxLength(200)
               .HasColumnType("varchar(200)");

        builder.Property(b => b.CPFCNPJPagador)
               .IsRequired(true)
               .HasMaxLength(20)
               .HasColumnType("varchar(20)");

        builder.Property(b => b.NomeBeneficiario)
               .IsRequired(true)
               .HasMaxLength(200)
               .HasColumnType("varchar(200)");

        builder.Property(b => b.CPFCNPJBeneficiario)
               .IsRequired(true)
               .HasMaxLength(20)
               .HasColumnType("varchar(20)");

        builder.Property(b => b.Valor)
               .IsRequired(true)
               .HasColumnType("numeric(18,2)");

        builder.Property(b => b.DataVencimento)
               .IsRequired(true)
               .HasColumnType("timestamp without time zone");

        builder.Property(b => b.Observacao)
               .IsRequired(false)
               .HasMaxLength(500)
               .HasColumnType("varchar(500)");

        builder.HasOne(b => b.Banco)
               .WithMany()
               .HasForeignKey(b => b.BancoId)
               .OnDelete(DeleteBehavior.Restrict);
    }
}