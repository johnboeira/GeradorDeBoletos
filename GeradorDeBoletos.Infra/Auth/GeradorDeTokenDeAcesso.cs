using GeradorDeBoletos.Domain.Features.Shared;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GeradorDeBoletos.Infra.Auth;

public class GeradorDeTokenDeAcesso : IGeradorDeTokenDeAcesso
{
    private readonly uint _minutosDeDuracao;
    private readonly string _chaveDeAssinatura;

    public GeradorDeTokenDeAcesso(uint minutosDeDuracao, string chaveDeAssinatura)
    {
        _minutosDeDuracao = minutosDeDuracao;
        _chaveDeAssinatura = chaveDeAssinatura;
    }

    public string Gerar(int id)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Sid, id.ToString())
        };

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.AddMinutes(_minutosDeDuracao),
            SigningCredentials = new SigningCredentials(SecurityKey(), SecurityAlgorithms.HmacSha256Signature)
        };

        var tokenHandler = new JwtSecurityTokenHandler();

        var secutiryToken = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(secutiryToken);
    }

    private SymmetricSecurityKey SecurityKey()
    {
        var bytes = Encoding.UTF8.GetBytes(_chaveDeAssinatura);

        return new SymmetricSecurityKey(bytes);
    }
}
