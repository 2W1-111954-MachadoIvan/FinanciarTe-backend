using FinanciarTeApi.Results;
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

        [HttpGet("getUsuarioByID/{legajo}")]
        public async Task<ActionResult> GetUsuarioByID(long legajo)
        {
            return Ok(await _serviceUsuario.GetUsuarioByID(legajo));
        }

        [HttpGet("getUsuarioByUser/{user}")]
        public async Task<ActionResult> GetUsuarioByUser(string user)
        {
            return Ok(await _serviceUsuario.GetUsuarioByUser(user));
        }

        [HttpGet("getViewUsuarios")]
        public async Task<ActionResult> GetViewUsuarios()
        {
            return Ok(await _serviceUsuario.GetViewUsuarios());
        }

        [HttpDelete]
        [Route("deleteSoftUsuario/{id}")]
        public async Task<ActionResult<ResultadoBase>> DeleteUsuario(int id)
        {
            //if (!_securityService.CheckUserHasroles(new string[] { "Admin" }))
            //    return StatusCode(StatusCodes.Status403Forbidden, "No tiene los permisos para ejecutar esta acción.");

            return Ok(await this._serviceUsuario.DeleteUsuario(id));
        }
    }
}
