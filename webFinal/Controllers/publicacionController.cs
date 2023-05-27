using Microsoft.AspNetCore.Mvc;
using webFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace webFinal.Controllers
{
    public class publicacionController : Controller
    {
        private readonly empleosDBContext _empleosDBContext;

        public publicacionController(empleosDBContext empleosDBContext1)
        {
            _empleosDBContext =  empleosDBContext1;
        }

        public ActionResult Index()
        {
            List<Publicacion> publi = _empleosDBContext.Publicaciones
                .Select(p => new Publicacion
                {
                    IdPublicacion = p.IdPublicacion,
                    IdEmpresa = p.IdEmpresa,
                    Titulo = p.Titulo,
                    Descripcion = p.Descripcion,
                    FechaPublicacion = p.FechaPublicacion
                })
                .ToList();

            return View(publi);
        }

    }

}
