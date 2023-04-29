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
    }
}
