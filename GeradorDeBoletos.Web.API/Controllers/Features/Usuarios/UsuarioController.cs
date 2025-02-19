using AutoMapper;
using GeradorDeBoletos.Services.Features.Boletos;
using Microsoft.AspNetCore.Mvc;

namespace GeradorDeBoletos.Web.API.Controllers.Features.Usuarios
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IMapper _mapper;

        public UsuarioController(IMapper mapper)
        {
            _mapper = mapper;
        }
    }
}