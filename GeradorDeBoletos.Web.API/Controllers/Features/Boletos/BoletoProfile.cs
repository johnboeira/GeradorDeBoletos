using AutoMapper;
using GeradorDeBoletos.Domain.Features.Boletos;
using GeradorDeBoletos.Web.API.DTOs.Features.Boletos;

namespace GeradorDeBoletos.Web.API.Controllers.Features.Boletos;

public class BoletoProfile : Profile
{
    public BoletoProfile()
    {
        CreateMap<BoletoCriarDTO, Boleto>();
        CreateMap<Boleto, BoletoResumoDTO>();
        CreateMap<Boleto, BoletoDetalheDTO>();
    }
}