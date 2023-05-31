using FinanciarTeApi.Commands;
using FinanciarTeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanciarTeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CuotasController : Controller
    {
        private readonly IServiceCuotas _servicioCobroCuotas;

        public CuotasController(IServiceCuotas servicioCobroCuotas)
        {
            _servicioCobroCuotas = servicioCobroCuotas;
        }

        [HttpGet("getCuota/{id}")]
        public async Task<ActionResult> GetCuotaByID(int id)
        {
            return Ok(await _servicioCobroCuotas.GetCuotaByID(id));
        }

        [HttpGet("getViewCuotasCliente/{id}")]
        public async Task<ActionResult> GetViewCuotasByCliente(int id)
        {
            return Ok(await _servicioCobroCuotas.GetViewCuotasByCliente(id));
        }

        [HttpGet("getCuotasByCliente/{id}")]
        public async Task<ActionResult> GetCuotasByCliente(int id)
        {
            return Ok(await _servicioCobroCuotas.GetCuotasByCliente(id));
        }

        [HttpPost("registrarCuotas/")]
        public async Task<ActionResult> RegistrarPagoCuotas([FromBody] ComandoCuota comando)
        {
            return Ok(await _servicioCobroCuotas.RegistrarPagoCuotas(comando));
        }

        [HttpPut("modificarCuota/")]
        public async Task<ActionResult> ModificarPagoCuotas([FromBody] ComandoCuota comando)
        {
            return Ok(await _servicioCobroCuotas.ModificarPagoCuotas(comando));
        }
    }
}
