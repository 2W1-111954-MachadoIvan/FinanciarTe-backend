using FinanciarTeApi.Commands;
using FinanciarTeApi.Models;
using FinanciarTeApi.Results;
using FinanciarTeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanciarTeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : Controller
    {
        private readonly IServiceCliente _servicioCliente;

        public ClienteController(IServiceCliente serviceCliente)
        {
            _servicioCliente = serviceCliente;
        }

        [HttpGet("getClientes")]
        public async Task<ActionResult> GetClientes()
        {
            return Ok(await _servicioCliente.GetClientes());
        }

        [HttpGet("getClienteByID/{id}")]
        public async Task<IActionResult> GetClienteByID(int id)
        {
            return Ok(await _servicioCliente.GetClienteByID(id));
        }

        [HttpGet("getViewClienteByID/{id}")]
        public async Task<IActionResult> GetViewClienteByID(int id)
        {
            return Ok(await _servicioCliente.GetViewClienteByID(id));
        }

        [HttpPost("postCliente")]
        public async Task<IActionResult> PostCliente([FromBody] ComandoCliente cliente)
        {
            var retVal = await _servicioCliente.PostCliente(cliente);

            if (retVal.Ok)
                return Ok(retVal);

            return BadRequest(retVal.Message);
        }

        [HttpPut("putCliente/{id}")]
        public async Task<IActionResult> PutCliente([FromBody] ComandoCliente cliente)
        {
            //if (!_securityService.CheckUserHasroles(new string[] { "Admin" }))
            //{
            //    return StatusCode(StatusCodes.Status403Forbidden, "No tiene los permisos para ejecutar esta acción.");
            //}
            //else
            //{
                var retVal = await _servicioCliente.PutCliente(cliente);

                if (retVal.Ok)
                    return Ok(retVal);

                return BadRequest(retVal.Message);
            //}
        }

        [HttpDelete]
        [Route("deleteSoftCliente/{id}")]

        public async Task<ActionResult<ResultadoBase>> DeleteCliente(int id)
        {
            //if (!_securityService.CheckUserHasroles(new string[] { "Admin" }))
            //    return StatusCode(StatusCodes.Status403Forbidden, "No tiene los permisos para ejecutar esta acción.");

            return Ok(await this._servicioCliente.DeleteCliente(id));
        }
    }
}
