using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using webFinal.Models;

namespace webFinal.Controllers
{
    public class ValoracionController : Controller
    {
        private readonly empleosDBContext _empleosDBContext;

        public ValoracionController(empleosDBContext context)
        {
            _empleosDBContext = context;
        }
        public IActionResult Index(string empresa)
        {
            List<ValoracionEmpresa> valoraciones = _empleosDBContext.ValoracionesEmpresas
     .Join(_empleosDBContext.Usuarios,
         valoracion => valoracion.IdUsuario,
         usuario => usuario.IdUsuario,
         (valoracion, usuario) => new
         {
             Valoracion = valoracion,
             Usuario = usuario
         })
     .Join(_empleosDBContext.Empresas,
         vu => vu.Valoracion.IdEmpresa,
         empresa => empresa.IdEmpresa,
         (vu, empresa) => new ValoracionEmpresa
         {
             IdValoracion = vu.Valoracion.IdValoracion,
             IdEmpresa = vu.Valoracion.IdEmpresa,
             IdUsuario = vu.Valoracion.IdUsuario,
             Comentario = vu.Valoracion.Comentario,
             Calificacion = vu.Valoracion.Calificacion,
             FechaValoracion = vu.Valoracion.FechaValoracion,
             Empresa = empresa,
             Usuario = vu.Usuario
         })
     .Where(v => string.IsNullOrEmpty(empresa) || v.Empresa.Nombre == empresa)
     .ToList();

            listados();

          

            return View(valoraciones);

        }


        public IActionResult TraerUsuario(string usuario)
        {

            var usuariosConValoraciones = _empleosDBContext.Usuarios
    .Join(_empleosDBContext.ValoracionesEmpresas,
        usuario => usuario.IdUsuario,
        valoracion => valoracion.IdUsuario,
        (usuario, valoracion) => new { Usuario = usuario })
    .Select(x => new { NombreUsuario = x.Usuario.Nombre })
    .ToList();




            return View(usuariosConValoraciones);
        }
        public void listados()
        {
            
            var lstEmpresa = new List<string>();

           

            List<Empresa> empresa = _empleosDBContext.Empresas
                .Where(p => !string.IsNullOrEmpty(p.Nombre)).Select(p => new Empresa
                {
                    Nombre = p.Nombre
                }).Distinct().ToList();

            
            foreach (var filtro in empresa)
            {

                if (!filtro.Nombre.IsNullOrEmpty())
                {
                    lstEmpresa.Add(filtro.Nombre);
                }

            }

          
            ViewData["listaEmpresa"] = new SelectList(lstEmpresa);
        }


    }

}
