using FinanciarTeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanciarTeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CiudadesController : Controller
    {
        private readonly IServiceCiudades _ciudadesService;

        public CiudadesController(IServiceCiudades ciudadesService)
        {
            _ciudadesService = ciudadesService;
        }

        [HttpGet("getCiudadesForComboBox/{id}")]
        public async Task<IActionResult> GetCiudadesForComboBoxItem(int id)
        {
            return Ok(await _ciudadesService.GetCiudadesForComboBox(id));
        }
    }
}
