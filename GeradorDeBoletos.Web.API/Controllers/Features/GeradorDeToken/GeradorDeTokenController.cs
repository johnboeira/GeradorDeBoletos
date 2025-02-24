using AutoMapper;
using GeradorDeBoletos.Domain.Features.GeradorDeToken;
using GeradorDeBoletos.Services.Features.GeradorDeToken;
using GeradorDeBoletos.Web.API.DTOs.Features.GeradorDeToken;
using Microsoft.AspNetCore.Mvc;

namespace GeradorDeBoletos.Web.API.Controllers.Features.GeradorDeToken;

[Route("api/[controller]")]
[ApiController]
public class GeradorDeTokenController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly GeradorDeTokenService _geradorDeTokenService;

    public GeradorDeTokenController(IMapper mapper, GeradorDeTokenService geradorDeTokenService)
    {
        _mapper = mapper;
        _geradorDeTokenService = geradorDeTokenService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    public async Task<IActionResult> Post([FromBody] LoginDTO usuarioDTO)
    {
        var login = _mapper.Map<Login>(usuarioDTO);

        var token = await _geradorDeTokenService.GerarTokenDeAcesso(login);

        return Created(string.Empty, token);
    }
}
