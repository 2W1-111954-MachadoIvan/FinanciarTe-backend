using Microsoft.AspNetCore.Mvc;

namespace FinanciarTeApi.Controllers
{
    public class TransaccionesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
