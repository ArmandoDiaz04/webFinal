using Microsoft.AspNetCore.Mvc;

namespace webFinal.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
