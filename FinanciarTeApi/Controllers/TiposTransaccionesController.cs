using FinanciarTeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanciarTeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TiposTransaccionesController : Controller
    {
        private readonly IServiceTipoTransaccion _tipoTranService;

        public TiposTransaccionesController(IServiceTipoTransaccion categoriaService)
        {
            _tipoTranService = categoriaService;
        }

        [HttpGet("getTipoTranForComboBox")]
        public async Task<IActionResult> GetTipoTransaccionForComboBoxItem()
        {
            return Ok(await _tipoTranService.GetTipoTransaccionForComboBox());
        }

    }
}
