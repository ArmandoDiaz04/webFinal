using Microsoft.AspNetCore.Mvc;
using webFinal.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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
            listados();


           
            List<Publicacion> publi = _empleosDBContext.Publicaciones
               .Join(_empleosDBContext.Empresas,
                   p => p.IdEmpresa,
                   e => e.IdEmpresa,
                   (p, e) => new
                   {
                       Publicacion = p,
                       Empresa = e
                   })
                 .Where(pe => string.IsNullOrEmpty(filtroTitulo) || pe.Publicacion.Titulo.Contains(filtroTitulo))
               .Select(pe => new Publicacion
               {
                   IdPublicacion = pe.Publicacion.IdPublicacion,
                   IdEmpresa = pe.Publicacion.IdEmpresa,
                   Titulo = pe.Publicacion.Titulo,
                   Descripcion = pe.Publicacion.Descripcion,
                   FechaPublicacion = pe.Publicacion.FechaPublicacion,
                   Empresa = new Empresa
                   {
                       IdEmpresa = pe.Empresa.IdEmpresa,
                       Nombre = pe.Empresa.Nombre,
                       Rubro = pe.Empresa.Rubro
                   }
               }).ToList();

            return View(publi);
        }
        public ActionResult DetallePublicacion(int id)
        {
            var publicacion = _empleosDBContext.Publicaciones
                .Join(_empleosDBContext.Empresas,
                    p => p.IdEmpresa,
                    e => e.IdEmpresa,
                    (p, e) => new
                    {
                        Publicacion = p,
                        Empresa = e
                    })
                .Where(pe => pe.Publicacion.IdPublicacion == id)
                .Select(pe => new Publicacion
                {
                    IdPublicacion = pe.Publicacion.IdPublicacion,
                    IdEmpresa = pe.Publicacion.IdEmpresa,
                    Titulo = pe.Publicacion.Titulo,
                    Descripcion = pe.Publicacion.Descripcion,
                    FechaPublicacion = pe.Publicacion.FechaPublicacion,
                    Empresa = new Empresa
                    {
                        IdEmpresa = pe.Empresa.IdEmpresa,
                        Nombre = pe.Empresa.Nombre,
                        Rubro = pe.Empresa.Rubro
                    }
                })
                .FirstOrDefault();

            if (publicacion == null)
            {
                return NotFound(); // Manejar si la publicación no existe
            }

            return View(publicacion);
        }


        public void listados()
        {
            var lstSalario = new List<string>();
            var lstTipoContrato = new List<string>();

            var filtros = _empleosDBContext.Publicaciones;
     List<Publicacion> salario = _empleosDBContext.Publicaciones
                    .Select(p => new Publicacion
                    {
                        Salario = p.Salario, // Asignar 0 si el valor de Salario es nulo
                    })
                    .Distinct()
                    .ToList();


            List<Publicacion> experiencia = _empleosDBContext.Publicaciones
                   .Select(p => new Publicacion
                   {
                       Experiencia = p.Experiencia ?? "", // Asignar una cadena vacía si el valor de Experiencia es nulo

                   })
                   .Distinct()
                   .ToList();
            List<Publicacion> localizacion = _empleosDBContext.Publicaciones
                    .Select(p => new Publicacion
                    {
                        Localizacion = p.Localizacion ?? "", // Asignar una cadena vacía si el valor de Localizacion es nulo

                    })
                    .Distinct()
                    .ToList();
            List<Publicacion> contrato = _empleosDBContext.Publicaciones
                .Where(p => !string.IsNullOrEmpty(p.tipo_contrato))
                    .Select(p => new Publicacion
                    {
                        tipo_contrato = p.tipo_contrato // Asignar una cadena vacía si el valor de tipo_contrato es nulo

                    })
                    .Distinct()
                    .ToList();


            foreach (var filtro in filtros)
            {
                if (filtro.Salario!=0)
                {
                    lstSalario.Add(filtro.Salario.ToString());
                }

                if (!string.IsNullOrEmpty(filtro.tipo_contrato))
                {
                    lstTipoContrato.Add(filtro.tipo_contrato);
                }
            }

            ViewData["listaSalario"] = new SelectList(lstSalario);
            ViewData["listaContrato"] = new SelectList(lstTipoContrato);
        }


    }

}
