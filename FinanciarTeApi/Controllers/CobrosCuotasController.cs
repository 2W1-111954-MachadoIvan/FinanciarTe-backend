using FinanciarTeApi.Commands;
using FinanciarTeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanciarTeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CobrosCuotasController : Controller
    {
        private readonly IServiceCobroCuotas _servicioCobroCuotas;

        public CobrosCuotasController(IServiceCobroCuotas servicioCobroCuotas)
        {
            _servicioCobroCuotas = servicioCobroCuotas;
        }

        [HttpGet("getCuotas/{id}")]
        public async Task<ActionResult> GetCuotasByCliente(int id)
        {
            return Ok(await _servicioCobroCuotas.GetCuotasByCliente(id));
        }

        [HttpPost("registrarCuotas/")]
        public async Task<ActionResult> RegistrarCuotas([FromBody] ComandoCobroCuota comando)
        {
            return Ok(await _servicioCobroCuotas.RegistrarCuotas(comando));
        }

        [HttpPut("modificarCuota/")]
        public async Task<ActionResult> ModificarCuotas([FromBody] ComandoCobroCuota comando)
        {
            return Ok(await _servicioCobroCuotas.ModificarCuota(comando));
        }
    }
}
