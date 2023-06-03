using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using webFinal.Models;

namespace webFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly empleosDBContext _dbContext;

        public HomeController(empleosDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ActionResult Index()
        {
            int numeroPublicaciones = _dbContext.Publicaciones.Count();
            int numeroEmpresas = _dbContext.Empresas.Count();
            int numeroUsuarios = _dbContext.Usuarios.Count();

            var model = new Estadisticas
            {
                NumeroPublicaciones = numeroPublicaciones,
                NumeroEmpresas = numeroEmpresas,
                NumeroUsuarios = numeroUsuarios
            };

            List<Publicacion> publicaciones = _dbContext.Publicaciones
    .OrderByDescending(p => p.FechaPublicacion)
    .Take(3)
    .ToList();

            var viewModel = new HomeALL
            {
                Estadisticas = model,
                Publicaciones = publicaciones
            };

            return View(viewModel);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}