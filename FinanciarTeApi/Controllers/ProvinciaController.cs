using FinanciarTeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanciarTeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProvinciaController : Controller
    {
        private readonly IServiceProvincia _provinciaService;

        public ProvinciaController(IServiceProvincia provinciaService)
        {
            _provinciaService = provinciaService;
        }

        [HttpGet("getProvinciasForComboBox")]
        public async Task<IActionResult> GetPronvinciasForComboBoxItem()
        {
            return Ok(await _provinciaService.GetProvinciasForComboBox());
        }
    }
}
