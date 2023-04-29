using FinanciarTeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanciarTeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EntidadesFinancierasController : Controller
    {
        private readonly IServiceEntidadesFinancieras _entFinService;

        public EntidadesFinancierasController(IServiceEntidadesFinancieras entFinService)
        {
            _entFinService = entFinService;
        }

        [HttpGet("getEntFinForComboBox")]
        public async Task<IActionResult> GetPronvinciasForComboBoxItem()
        {
            return Ok(await _entFinService.GetEntFinForComboBox());
        }
    }
}
