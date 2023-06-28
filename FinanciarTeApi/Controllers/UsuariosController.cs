using FinanciarTeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanciarTeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController : Controller
    {
        private readonly IServiceUsuario _serviceUsuario;

        public UsuariosController(IServiceUsuario serviceUsuario)
        {
            _serviceUsuario = serviceUsuario;
        }

        [HttpGet("getUsuarios")]
        public async Task<ActionResult> GetUsuarios()
        {
            return Ok(await _serviceUsuario.GetUsuarios());
        }

        [HttpGet("getViewUsuarios")]
        public async Task<ActionResult> GetViewUsuarios()
        {
            return Ok(await _serviceUsuario.GetViewUsuarios());
        }
    }
}
