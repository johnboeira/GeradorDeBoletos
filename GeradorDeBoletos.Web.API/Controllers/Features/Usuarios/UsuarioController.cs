using AutoMapper;
using GeradorDeBoletos.Domain.Features.Usuarios;
using GeradorDeBoletos.Web.API.DTOs.Features.Usuarios;
using GeradorDeBoletos.Services.Features.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace GeradorDeBoletos.Web.API.Controllers.Features.Usuarios;

[Route("api/[controller]")]
[ApiController]
public class UsuarioController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly UsuarioService _usuarioService;

    public UsuarioController(IMapper mapper, UsuarioService usuarioService)
    {
        _mapper = mapper;
        _usuarioService = usuarioService;
    }

    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] UsuarioCriarDTO usuarioDTO)
    {
        var usuario = _mapper.Map<Usuario>(usuarioDTO);

        var email = await _usuarioService.CriarUsuarioAsync(usuario);

        return Created(string.Empty, email);
    }
}