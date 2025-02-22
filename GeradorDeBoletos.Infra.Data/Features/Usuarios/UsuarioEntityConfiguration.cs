using GeradorDeBoletos.Domain.Features.Bancos;
using GeradorDeBoletos.Domain.Features.Usuarios;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GeradorDeBoletos.Infra.Data.Features.Usuarios;

public class UsuarioEntityConfiguration : IEntityTypeConfiguration<Usuario>
{
    public void Configure(EntityTypeBuilder<Usuario> builder)
    {
        builder.ToTable("usuarios");

        builder.HasKey(b => b.Id);

        builder.Property(b => b.Id)
               .ValueGeneratedOnAdd();

        builder.Property(b => b.Email)
               .IsRequired(true)
               .HasMaxLength(100)
               .HasColumnType("varchar(100)");

        builder.Property(b => b.Senha)
               .IsRequired(true)
               .HasMaxLength(300)
               .HasColumnType("varchar(300)");
    }
}