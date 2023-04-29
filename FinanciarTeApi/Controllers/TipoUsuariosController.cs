using FinanciarTeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanciarTeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoUsuariosController : Controller
    {
        private readonly IServiceTiposUsuarios _tipoUsuService;

        public TipoUsuariosController(IServiceTiposUsuarios tipoUsuService)
        {
            _tipoUsuService = tipoUsuService;
        }

        [HttpGet("getTipoUsuForComboBox")]
        public async Task<IActionResult> GetTipoUsuForComboBoxItem()
        {
            return Ok(await _tipoUsuService.GetTipoUsuForComboBox());
        }
    }
}
