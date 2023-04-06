using Microsoft.AspNetCore.Mvc;

namespace FinanciarTeApi.Controllers
{
    public class DetalleTransaccionesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
