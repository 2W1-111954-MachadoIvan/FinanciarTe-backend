using Microsoft.AspNetCore.Mvc;

namespace FinanciarTeApi.Controllers
{
    public class UsuariosController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
