using FluentValidation;
using FluentValidation.AspNetCore;
using GeradorDeBoletos.Infra.Data;
using GeradorDeBoletos.Infra.Data.Features.Bancos;
using GeradorDeBoletos.Infra.Data.Features.Boletos;
using GeradorDeBoletos.Services.Features.Bancos;
using GeradorDeBoletos.Services.Features.Boletos;
using GeradorDeBoletos.Web.API.DTOs.Features.Bancos;
using GeradorDeBoletos.Web.API.Extensions;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace GeradorDeBoletos.Web.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Adiciona dependências
        builder.Services.AddDbContext<GerardorDeBoletosDbContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<BancoRepository>();
        builder.Services.AddScoped<BancoService>();

        builder.Services.AddScoped<BoletoRepository>();
        builder.Services.AddScoped<BoletoService>();

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Error()
            .WriteTo.Console()
            .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        builder.Host.UseSerilog();

        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        builder.Services.AddControllers();

        builder.Services.AddFluentValidationAutoValidation();
        builder.Services.AddValidatorsFromAssemblyContaining<BancoCriarDTO>();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<GerardorDeBoletosDbContext>();
            dbContext.Database.Migrate();
        }

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware(typeof(GlobalErrorHandlingMiddleware));

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}