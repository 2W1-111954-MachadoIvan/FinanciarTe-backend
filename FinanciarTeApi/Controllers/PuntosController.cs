using FinanciarTeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanciarTeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PuntosController : Controller
    {
        private readonly IServicePuntos _servicePuntos;

        public PuntosController(IServicePuntos servicePuntos)
        {
            _servicePuntos = servicePuntos;
        }

        [HttpGet("getHistoricoPuntos")]
        public async Task<ActionResult> GetHistoricoPuntos()
        {
            return Ok(await _servicePuntos.GetHistoricoPuntos());
        }

        [HttpGet("getHistoricoPuntosByCliente/{id}")]
        public async Task<ActionResult> GetHistoricoPuntosByCliente(long id)
        {
            return Ok(await _servicePuntos.GetHistoricoPuntosByCliente(id));
        }
    }
}
