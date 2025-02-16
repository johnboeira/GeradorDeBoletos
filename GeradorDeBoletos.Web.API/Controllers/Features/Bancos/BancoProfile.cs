using AutoMapper;
using GeradorDeBoletos.Domain.Features.Bancos;
using GeradorDeBoletos.Web.API.DTOs.Features.Bancos;

namespace GeradorDeBoletos.Web.API.Controllers.Features.Bancos;

public class BancoProfile : Profile
{
    public BancoProfile()
    {
        CreateMap<BancoCriarDTO, Banco>();
        CreateMap<Banco, BancoDetalheDTO>();
        CreateMap<Banco, BancoResumoDTO>();
    }
}