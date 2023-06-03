using Microsoft.AspNetCore.Mvc;
using webFinal.Models;

namespace webFinal.Controllers
{
    public class EmpresaController : Controller
    {
        
        private readonly empleosDBContext _dbContext;

        public EmpresaController(empleosDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult CrearEmpresa(Empresa empresa)
        {
            _dbContext.Add(empresa);
            _dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {

            return View();
        }
    }
}
