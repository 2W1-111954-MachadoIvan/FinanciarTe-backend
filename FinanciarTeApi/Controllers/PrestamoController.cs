using FinanciarTeApi.Commands;
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

        [HttpGet("getPrestamosByCliente/{id}")]
        public async Task<ActionResult> GetPrestamosByCliente(int id)
        {
            return Ok(await _servicioPrestamo.GetPrestamosByCliente(id));
        }

        [HttpGet("getPrestamosByIdToMod/{id}")]
        public async Task<ActionResult> getPrestamosByIdToMod(int id)
        {
            return Ok(await _servicioPrestamo.getPrestamosByIdToMod(id));
        }

        [HttpGet("getPrestamos/")]
        public async Task<ActionResult> GetPrestamos()
        {
            return Ok(await _servicioPrestamo.GetPrestamos());
        }

        [HttpGet("getPrestamoCuotasById/{id}")]
        public async Task<ActionResult> GetPrestamosCuotasById(int id)
        {
            return Ok(await _servicioPrestamo.GetPrestamoCuotasByID(id));
        }

        [HttpPost("registrarPrestamo/")]
        public async Task<ActionResult> RegistrarPrestamo(ComandoPrestamo comando)
        {
            return Ok(await _servicioPrestamo.RegistrarPrestamo(comando));
        }

        [HttpPut("modificarPrestamo/{id}")]
        public async Task<ActionResult> ModificarPrestamo(ComandoPrestamo comando)
        {
            return Ok(await _servicioPrestamo.ModificarPrestamo(comando));
        }
    }
}
