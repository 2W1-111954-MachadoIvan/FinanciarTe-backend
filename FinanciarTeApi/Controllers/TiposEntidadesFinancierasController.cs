using FinanciarTeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanciarTeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TiposEntidadesFinancierasController : Controller
    {
        private readonly IServiceTiposEntidadFinanciera _entFinService;

        public TiposEntidadesFinancierasController(IServiceTiposEntidadFinanciera entFinService)
        {
            _entFinService = entFinService;
        }

        [HttpGet("getTipoEntFinForComboBox")]
        public async Task<IActionResult> GetTipoEntFinForComboBoxItem()
        {
            return Ok(await _entFinService.GetTipoEntFinForComboBox());
        }
    }
}
