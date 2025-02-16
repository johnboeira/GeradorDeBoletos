﻿// <auto-generated />
using System;
using GeradorDeBoletos.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GeradorDeBoletos.Infra.Data.Migrations
{
    [DbContext(typeof(GerardorDeBoletosDbContext))]
    partial class GerardorDeBoletosDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.36")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("GeradorDeBoletos.Domain.Features.Bancos.Banco", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("CodigoBanco")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("NomeBanco")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("PercentualDeJuros")
                        .HasColumnType("numeric(18,2)");

                    b.HasKey("Id");

                    b.ToTable("bancos", (string)null);
                });

            modelBuilder.Entity("GeradorDeBoletos.Domain.Features.Boletos.Boleto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("BancoId")
                        .HasColumnType("integer");

                    b.Property<string>("CPFCNPJBeneficiario")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("CPFCNPJPagador")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.Property<DateTime>("DataVencimento")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("NomeBeneficiario")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("NomePagador")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Observacao")
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("numeric(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("BancoId");

                    b.ToTable("boletos", (string)null);
                });

            modelBuilder.Entity("GeradorDeBoletos.Domain.Features.Boletos.Boleto", b =>
                {
                    b.HasOne("GeradorDeBoletos.Domain.Features.Bancos.Banco", "Banco")
                        .WithMany()
                        .HasForeignKey("BancoId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Banco");
                });
#pragma warning restore 612, 618
        }
    }
}
