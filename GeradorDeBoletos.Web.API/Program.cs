using FluentValidation;
using FluentValidation.AspNetCore;
using GeradorDeBoletos.Domain.Features.Bancos;
using GeradorDeBoletos.Domain.Features.Boletos;
using GeradorDeBoletos.Domain.Features.Shared;
using GeradorDeBoletos.Domain.Features.Usuarios;
using GeradorDeBoletos.Infra.Auth;
using GeradorDeBoletos.Infra.Criptografia;
using GeradorDeBoletos.Infra.Data;
using GeradorDeBoletos.Infra.Data.Features.Bancos;
using GeradorDeBoletos.Infra.Data.Features.Boletos;
using GeradorDeBoletos.Infra.Data.Features.Usuarios;
using GeradorDeBoletos.Services.Features.Bancos;
using GeradorDeBoletos.Services.Features.Boletos;
using GeradorDeBoletos.Services.Features.GeradorDeToken;
using GeradorDeBoletos.Services.Features.Usuarios;
using GeradorDeBoletos.Web.API.DTOs.Features.Bancos;
using GeradorDeBoletos.Web.API.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
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

        builder.Services.AddScoped<IBancoRepository, BancoRepository>();
        builder.Services.AddScoped<BancoService>();

        builder.Services.AddScoped<IBoletoRepository, BoletoRepository>();
        builder.Services.AddScoped<BoletoService>();

        builder.Services.AddScoped<SenhaEncriptador>();
        builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
        builder.Services.AddScoped<UsuarioService>();

        var minutosDeDuracao = builder.Configuration.GetValue<uint>("Settings:JWT:minutosDeDuracao");
        var chaveDeAssinatura = builder.Configuration.GetValue<string>("Settings:JWT:chaveDeAssinatura");

        builder.Services.AddScoped<IGeradorDeTokenDeAcesso>(
            options => new GeradorDeTokenDeAcesso(minutosDeDuracao, chaveDeAssinatura));
        builder.Services.AddScoped<GeradorDeTokenService>();
        builder.Services.AddScoped<IValidadorToken>(opt => new ValidadorToken(chaveDeAssinatura));

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
        builder.Services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                Description = "Insira 'Bearer' [espaço] e o token JWT. Exemplo: 'Bearer eyJhbGciOiJI...'"
            });
            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });
        });

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