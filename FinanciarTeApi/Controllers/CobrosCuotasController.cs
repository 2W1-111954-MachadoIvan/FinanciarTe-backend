using Microsoft.AspNetCore.Mvc;

namespace FinanciarTeApi.Controllers
{
    public class CobrosCuotasController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
