using FinanciarTeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanciarTeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : Controller
    {
        private readonly IServiceCategoria _categoriaService;

        public CategoriaController(IServiceCategoria categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [HttpGet("getCategoriasForComboBox")]
        public async Task<IActionResult> GetCategoriasForComboBoxItem()
        {
            return Ok(await _categoriaService.GetCategoriasForComboBox());
        }
    }
}
