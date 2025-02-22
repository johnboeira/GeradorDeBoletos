using AutoMapper;
using GeradorDeBoletos.Domain.Features.GeradorDeToken;
using GeradorDeBoletos.Web.API.DTOs.Features.GeradorDeToken;

namespace GeradorDeBoletos.Web.API.Controllers.Features.GeradorDeToken
{
    public class GeradorDeTokenProfile : Profile
    {
        public GeradorDeTokenProfile()
        {
            CreateMap<LoginDTO, Login>();
        }
    }
}
