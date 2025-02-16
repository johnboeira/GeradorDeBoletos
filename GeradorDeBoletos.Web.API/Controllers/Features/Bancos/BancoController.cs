using AutoMapper;
using GeradorDeBoletos.Domain.Features.Bancos;
using GeradorDeBoletos.Services.Features.Bancos;
using GeradorDeBoletos.Web.API.DTOs.Features.Bancos;
using Microsoft.AspNetCore.Mvc;

namespace GeradorDeBoletos.Web.API.Controllers.Features.Bancos;

[Route("api/[controller]")]
[ApiController]
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
    public async Task<IActionResult> Get()
    {
        var bancosDB = await _bancoService.BuscaTodos();

        var bancosDTO = _mapper.Map<List<BancoDetalheDTO>>(bancosDB);

        return Ok(bancosDTO);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var bancoDB = await _bancoService.Busca(id);

        var bancoDTO = _mapper.Map<BancoDetalheDTO>(bancoDB);

        return Ok(bancoDTO);
    }

    //400 - fluent
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] BancoCriarDTO bancoDTO)
    {
        var banco = _mapper.Map<Banco>(bancoDTO);

        await _bancoService.CriarBancoAsync(banco);

        return Ok();
    }
}