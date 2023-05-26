using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace webFinal.Controllers
{
    public class descripcionController : Controller
    {
        public IActionResult Index()
        {

            var lstSalario = new List<string>();
            lstSalario.Add("1000");
            lstSalario.Add("2000");
            lstSalario.Add("3000");

            ViewData["listaSalario"] = new SelectList(lstSalario);

            return View();

        }
    }
}
