using GeradorDeBoletos.Domain.Features.Shared;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace GeradorDeBoletos.Infra.Auth;

public class ValidadorToken : GeradorDeChaveDeSeguranca, IValidadorToken
{
    private readonly string _chaveDeAssinatura;

    public ValidadorToken(string chaveDeAssinatura)
    {
        _chaveDeAssinatura = chaveDeAssinatura;
    }

    public int Validar(string token)
    {
        var validacaoDeParametros = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = GerarSecurityKey(_chaveDeAssinatura),
            ClockSkew = new TimeSpan(0)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var principalClaim = tokenHandler.ValidateToken(token, validacaoDeParametros, out _);

        var idDoUsuario = principalClaim.Claims.First(c => c.Type == ClaimTypes.Sid).Value;

        return int.Parse(idDoUsuario);
    }
}
