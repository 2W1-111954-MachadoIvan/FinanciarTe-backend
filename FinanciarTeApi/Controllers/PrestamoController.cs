using FinanciarTeApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace FinanciarTeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrestamoController : Controller
    {
        private readonly IServicePrestamo _servicioPrestamo;

        public PrestamoController(IServicePrestamo servicioPrestamo)
        {
            _servicioPrestamo = servicioPrestamo;
        }

        [HttpGet("getPrestamo/{id}")]
        public async Task<ActionResult> GetPrestamosByCliente(int id)
        {
            return Ok(await _servicioPrestamo.GetPrestamosByCliente(id));
        }


    }
}
