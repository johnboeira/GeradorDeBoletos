using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace GeradorDeBoletos.Infra.Data.Migrations
{
    public partial class addtabelabancosboletos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bancos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NomeBanco = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    CodigoBanco = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    PercentualDeJuros = table.Column<decimal>(type: "numeric(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bancos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "boletos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NomePagador = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    CPFCNPJPagador = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    NomeBeneficiario = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false),
                    CPFCNPJBeneficiario = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    Valor = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Observacao = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true),
                    BancoId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_boletos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_boletos_bancos_BancoId",
                        column: x => x.BancoId,
                        principalTable: "bancos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_boletos_BancoId",
                table: "boletos",
                column: "BancoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "boletos");

            migrationBuilder.DropTable(
                name: "bancos");
        }
    }
}
