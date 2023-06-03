using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

            var empresasConValoraciones = _empleosDBContext.ValoracionesEmpresas
     .Join(_empleosDBContext.Empresas,
         valoracion => valoracion.IdEmpresa,
         empresa => empresa.IdEmpresa,
         (valoracion, empresa) => new { Valoracion = valoracion, Empresa = empresa })
     .Select(x => new { NombreEmpresa = x.Empresa.Nombre,
     valor = new valor
     {
        Comentario = valor.Comentario,
        
     }
     })
     .ToList();



            return View(empresasConValoraciones);
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
