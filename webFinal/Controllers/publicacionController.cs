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
            _empleosDBContext = empleosDBContext1;
        }

        public ActionResult Index(string filtroTitulo)
        {
            var lstSalario = new List<string>();
            lstSalario.Add("100");
            lstSalario.Add("200");
            lstSalario.Add("300");
            lstSalario.Add("400");
            lstSalario.Add("400");

            ViewData["listaSalario"] = new SelectList(lstSalario);


            List<Publicacion> publi = _empleosDBContext.Publicaciones
             .Where(p => string.IsNullOrEmpty(filtroTitulo) || p.Titulo.Contains(filtroTitulo))
             .Select(p => new Publicacion
             {
                 IdPublicacion = p.IdPublicacion,
                 IdEmpresa = p.IdEmpresa,
                 Titulo = p.Titulo,
                 Descripcion = p.Descripcion,
                 FechaPublicacion = p.FechaPublicacion,
               //  NombreEmpresa = p.Empresa.Nombre  // Obtener el nombre de la empresa asociada
             })
             .ToList();

            return View(publi);
        }


    }

}
