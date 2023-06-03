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

        public ActionResult Index(string filtroTitulo, string valorLocaliza, string valorSalario, string valorExperiencia, string valorEmpresa, string tipoContrato)
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
                .Where(pe =>
                    (string.IsNullOrEmpty(filtroTitulo) || pe.Publicacion.Titulo.Contains(filtroTitulo)) &&
                    (string.IsNullOrEmpty(valorLocaliza) || pe.Publicacion.Localizacion == valorLocaliza) &&
                    (string.IsNullOrEmpty(valorSalario) || pe.Publicacion.Salario.ToString() == valorSalario) &&
                    (string.IsNullOrEmpty(valorExperiencia) || pe.Publicacion.Experiencia == valorExperiencia) &&
                    (string.IsNullOrEmpty(valorEmpresa) || pe.Empresa.Nombre == valorEmpresa) &&
                    (string.IsNullOrEmpty(tipoContrato) || pe.Publicacion.tipo_contrato == tipoContrato)
                )
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
                .ToList();

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
            var lstExperiencia = new List<string>();
            var lstLocalizacion = new List<string>();
            var lstEmpresa = new List<string>();

            List<Publicacion> salario = _empleosDBContext.Publicaciones
                           .Select(p => new Publicacion
                           {
                               Salario = p.Salario, // Asignar 0 si el valor de Salario es nulo
                           })
                           .Distinct()
                           .ToList();


            List<Publicacion> experiencia = _empleosDBContext.Publicaciones
                 .Where(p => !string.IsNullOrEmpty(p.Experiencia))
                 .Select(p => new Publicacion
                 {
                     Experiencia = p.Experiencia ?? "", // Assign an empty string if the value of Experiencia is null
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

            List<Empresa> empresa = _empleosDBContext.Empresas
                .Where(p => !string.IsNullOrEmpty(p.Nombre)).Select(p => new Empresa { 
                Nombre = p.Nombre}).Distinct().ToList();

            foreach (var item in salario)
            {
                if (!item.Salario.ToString().IsNullOrEmpty())
                {
                    lstSalario.Add(item.Salario.ToString());
                }
            }
            foreach (var item in experiencia)
            {
                if (item != null && !string.IsNullOrEmpty(item.Experiencia))
                {
                    lstExperiencia.Add(item.Experiencia);
                }
            }   
            foreach (var item in localizacion)
            {
                if (item != null && !string.IsNullOrEmpty(item.Localizacion?.ToString()))
                {
                    lstLocalizacion.Add(item.Localizacion);
                }
            }
            foreach (var filtro in contrato)
            {

                if (!filtro.tipo_contrato.IsNullOrEmpty())
                {
                    lstTipoContrato.Add(filtro.tipo_contrato);
                }                              
               
            }
            foreach (var filtro in empresa)
            {

                if (!filtro.Nombre.IsNullOrEmpty())
                {
                    lstEmpresa.Add(filtro.Nombre);
                }                              
               
            }

            ViewData["listaSalario"] = new SelectList(lstSalario);
            ViewData["listaContrato"] = new SelectList(lstTipoContrato);
            ViewData["listaExperiencia"] = new SelectList(lstExperiencia);
            ViewData["listaLocalizacion"] = new SelectList(lstLocalizacion);
            ViewData["listaEmpresa"] = new SelectList(lstEmpresa);
        }


    }

}
