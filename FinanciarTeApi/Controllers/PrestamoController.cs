using Microsoft.AspNetCore.Mvc;

namespace FinanciarTeApi.Controllers
{
    public class PrestamoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
