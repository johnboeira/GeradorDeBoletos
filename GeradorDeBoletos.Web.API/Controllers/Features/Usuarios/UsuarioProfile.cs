using AutoMapper;
using GeradorDeBoletos.Domain.Features.Usuarios;
using GeradorDeBoletos.Web.API.DTOs.Features.Usuarios;

namespace GeradorDeBoletos.Web.API.Controllers.Features.Usuarios;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<UsuarioCriarDTO, Usuario>();
    }
}
