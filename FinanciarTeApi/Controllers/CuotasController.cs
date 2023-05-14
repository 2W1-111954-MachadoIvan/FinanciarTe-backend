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

        [HttpGet("getCuotas/{id}")]
        public async Task<ActionResult> GetCuotasByCliente(int id)
        {
            return Ok(await _servicioCobroCuotas.GetCuotasByCliente(id));
        }

        [HttpPost("registrarCuotas/")]
        public async Task<ActionResult> RegistrarCuotas([FromBody] ComandoCuota comando)
        {
            return Ok(await _servicioCobroCuotas.RegistrarCuotas(comando));
        }

        [HttpPut("modificarCuota/")]
        public async Task<ActionResult> ModificarCuotas([FromBody] ComandoCuota comando)
        {
            return Ok(await _servicioCobroCuotas.ModificarCuota(comando));
        }
    }
}
