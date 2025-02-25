using AutoMapper;
using FluentValidation;
using GeradorDeBoletos.Domain.Features.Bancos;
using GeradorDeBoletos.Domain.Features.Shared.Exceptions;
using GeradorDeBoletos.Services.Features.Bancos;
using GeradorDeBoletos.Web.API.Attributes;
using GeradorDeBoletos.Web.API.DTOs.Features.Bancos;
using GeradorDeBoletos.Web.API.DTOs.Features.Boletos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GeradorDeBoletos.Web.API.Controllers.Features.Bancos;

[Authentication]
[Route("api/[controller]")]
[ApiController]
[ProducesResponseType(typeof(InvalidLogin), StatusCodes.Status401Unauthorized)]
[ProducesResponseType(typeof(SecurityTokenInvalidSignatureException), StatusCodes.Status401Unauthorized)]
public class BancoController : ControllerBase
{
    private BancoService _bancoService;
    private readonly IMapper _mapper;

    public BancoController(BancoService bancoService, IMapper mapper)
    {
        _bancoService = bancoService;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<BoletoResumoDTO>), StatusCodes.Status200OK)]
    public async Task<IActionResult> BuscaTodos()
    {
        var bancosDB = await _bancoService.BuscaTodos();

        var bancosDTO = _mapper.Map<List<BancoDetalheDTO>>(bancosDB);

        return Ok(bancosDTO);
    }

    [HttpGet("{codigo}")]
    [ProducesResponseType(typeof(BoletoDetalheDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(NotFoundException), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> BuscaPorCodigo(string codigo)
    {
        var bancoDB = await _bancoService.BuscaPorCodigoAsync(codigo);

        var bancoDTO = _mapper.Map<BancoDetalheDTO>(bancoDB);

        return Ok(bancoDTO);
    }

    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationException), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Cria([FromBody] BancoCriarDTO bancoDTO)
    {
        var banco = _mapper.Map<Banco>(bancoDTO);

        await _bancoService.CriarBancoAsync(banco);

        return Ok();
    }
}