using Microsoft.AspNetCore.Mvc;

namespace FinanciarTeApi.Controllers
{
    public class PuntosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
