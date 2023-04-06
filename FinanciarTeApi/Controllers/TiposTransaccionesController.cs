using Microsoft.AspNetCore.Mvc;

namespace FinanciarTeApi.Controllers
{
    public class TiposTransaccionesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
