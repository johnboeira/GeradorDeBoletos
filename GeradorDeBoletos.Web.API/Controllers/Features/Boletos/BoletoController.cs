using AutoMapper;
using FluentValidation;
using GeradorDeBoletos.Domain.Features.Boletos;
using GeradorDeBoletos.Domain.Features.Shared.Exceptions;
using GeradorDeBoletos.Services.Features.Boletos;
using GeradorDeBoletos.Web.API.Attributes;
using GeradorDeBoletos.Web.API.DTOs.Features.Boletos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;


namespace GeradorDeBoletos.Web.API.Controllers.Features.Boletos;

[Route("api/[controller]")]
[ApiController]
[Authentication]
[ProducesResponseType(typeof(InvalidLogin), StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(SecurityTokenInvalidSignatureException), StatusCodes.Status401Unauthorized)]
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
    [ProducesResponseType(typeof(List<BoletoResumoDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> BuscaTodos()
    {
        var boletosDB = await _boletoService.BuscaTodos();

        var boletosDTO = _mapper.Map<List<BoletoResumoDTO>>(boletosDB);

        return Ok(boletosDTO);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(BoletoDetalheDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundException), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> BuscaPorId(int id)
    {
        var boletoDB = await _boletoService.Busca(id);

        var boletoDTO = _mapper.Map<BoletoDetalheDTO>(boletoDB);

        return Ok(boletoDTO);
    }

    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationException), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(NotFoundException), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Cria([FromBody] BoletoCriarDTO boletoDTO)
    {
        var boleto = _mapper.Map<Boleto>(boletoDTO);

        await _boletoService.CriarBoletoAsync(boleto);

        return Created(string.Empty, boleto.Id);
    }
}