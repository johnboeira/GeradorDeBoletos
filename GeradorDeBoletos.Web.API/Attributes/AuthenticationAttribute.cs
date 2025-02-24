using Microsoft.AspNetCore.Mvc;

namespace GeradorDeBoletos.Web.API.Attributes;

public class AuthenticationAttribute : TypeFilterAttribute
{
    public AuthenticationAttribute() : base(typeof(AuthenticatorFilter))
    {
    }
}
