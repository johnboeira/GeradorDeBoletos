using AutoMapper;
using GeradorDeBoletos.Domain.Features.Boletos;
using GeradorDeBoletos.Services.Features.Boletos;
using GeradorDeBoletos.Web.API.DTOs.Features.Boletos;
using Microsoft.AspNetCore.Mvc;

namespace GeradorDeBoletos.Web.API.Controllers.Features.Boletos;

[Route("api/[controller]")]
[ApiController]
public class BoletoController : ControllerBase
{
    private BoletoService _boletoService;
    private readonly IMapper _mapper;

    public BoletoController(BoletoService boletoService, IMapper mapper)
    {
        _boletoService = boletoService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var boletosDB = await _boletoService.BuscaTodos();

        var boletosDTO = _mapper.Map<List<BoletoResumoDTO>>(boletosDB);

        return Ok(boletosDTO);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var boletoDB = await _boletoService.Busca(id);

        var boletoDTO = _mapper.Map<BoletoDetalheDTO>(boletoDB);

        return Ok(boletoDTO);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] BoletoCriarDTO boletoDTO)
    {
        var boleto = _mapper.Map<Boleto>(boletoDTO);

        await _boletoService.CriarBoletoAsync(boleto);

        return Ok();
    }
}