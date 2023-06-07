using FinanciarTeApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FinanciarTeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : Controller
    {
        private readonly IServiceReportes _serviceReportes;

        public ReportesController(IServiceReportes serviceReportes)
        {
            _serviceReportes = serviceReportes;
        }

        [HttpGet("getValoresDolarIndice")]
        public async Task<ActionResult> GetValoresHistoricosDolar()
        {
            return Ok(await _serviceReportes.GetValoresHistoricosDolar());
        }

        [HttpGet("getMaxMInDolarIndice")]
        public async Task<ActionResult> GetMaxMinDolarIndice()
        {
            return Ok(await _serviceReportes.GetMaxMinDolarIndice());
        }

        [HttpGet("getClientesConMasPrestamos")]
        public async Task<ActionResult> GetClientesConMasPrestamos()
        {
            return Ok(await _serviceReportes.GetClientesConMasPrestamos());
        }

        [HttpGet("getRecaudacionMensual")]
        public async Task<ActionResult> GetRecaudacionMensual()
        {
            return Ok(await _serviceReportes.GetRecaudacionMensual());
        }

        [HttpGet("getBalance")]
        public async Task<ActionResult> GetBalance()
        {
            return Ok(await _serviceReportes.GetBalance());
        }

        [HttpGet("getCuotasMesEnCurso")]
        public async Task<ActionResult> GetCuotasMesEnCurso()
        {
            return Ok(await _serviceReportes.GetCuotasMesEnCurso());
        }


    }
}
