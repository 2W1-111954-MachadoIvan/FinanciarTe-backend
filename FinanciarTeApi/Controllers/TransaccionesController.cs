using FinanciarTeApi.Commands;
using FinanciarTeApi.DataTransferObjects;
using FinanciarTeApi.Models;
using FinanciarTeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanciarTeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransaccionesController : Controller
    {
        private readonly IServiceTransacciones _servicioTransaccion;

        public TransaccionesController(IServiceTransacciones serviceTransaccion)
        {
            _servicioTransaccion = serviceTransaccion;
        }

        [HttpPost("registrarTransaccion")]
        public async Task<ActionResult> RegistrarTransaccion([FromBody] ComandoTransaccion comando)
        {
            return Ok(await _servicioTransaccion.RegistrarTransaccion(comando));
        }

        [HttpPut("modificarTransaccion")]
        public async Task<ActionResult> ModificarTransaccion([FromBody] ComandoTransaccion comando)
        {
            return Ok(await _servicioTransaccion.ModificarTransaccion(comando));
        }

        [HttpPut("deleteSoftTransaccion")]
        public async Task<ActionResult> DeleteSoftTransaccion(ComandoAnulaciones anulacion)
        {
            return Ok(await _servicioTransaccion.DeleteSoftTransaccion(anulacion));
        }

        [HttpGet("getTransacciones")]
        public async Task<ActionResult> GetListadoTransacciones()
        {
            return Ok(await _servicioTransaccion.GetListadoTransacciones());
        }

        [HttpGet("getTransaccionById/{id}")]
        public async Task<ActionResult> GetTransaccionById(int id)
        {
            return Ok(await _servicioTransaccion.GetTransaccionById(id));
        }
    }
}
