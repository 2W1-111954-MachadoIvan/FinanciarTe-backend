using FinanciarTeApi.Commands;
using FinanciarTeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanciarTeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DolarController : Controller
    {
        private readonly IServiceDolar _servicioDolar;

        public DolarController(IServiceDolar serviceDolar)
        {
            _servicioDolar = serviceDolar;
        }

        [HttpGet("getHistoricoDolarIndice")]
        public async Task<ActionResult> GetValoresHistoricosDolar()
        {
            return Ok(await _servicioDolar.GetValoresHistoricosDolar());
        }

        [HttpGet("getUltimoDolarIndice")]
        public async Task<ActionResult> GetUltimoValorDolar()
        {
            return Ok(await _servicioDolar.GetUltimoValorDolar());
        }

        [HttpGet("getValorDolarHoy")]
        public async Task<ActionResult> GetValorDolarHoy()
        {
            return Ok(await _servicioDolar.GetValorDolarHoy());
        }
    }
}
