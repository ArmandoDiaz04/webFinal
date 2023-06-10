    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using webFinal.Models;

    namespace webFinal.Controllers
    {
        public class PostulacionController : Controller
        {
            private readonly empleosDBContext _dbContext;

            public PostulacionController(empleosDBContext dbContext)
            {
                _dbContext = dbContext;
            }
        public IActionResult Index()
        {
         
            return View();
        }


        [HttpPost]
            public IActionResult Crear(int IdPublicacion, int IdEmpresa, int IdUsuario)
            {
                // Aquí puedes realizar la lógica para crear la postulación
                // utilizando los datos recibidos

                // Por ejemplo:
                var postulacion = new Postulacion
                {
                    IdPublicacion = IdPublicacion,
                    IdEmpresa = IdEmpresa,
                    IdUsuario = IdUsuario,
                    FechaPostulacion = DateTime.Now
                };

                _dbContext.postulaciones.Add(postulacion);
                _dbContext.SaveChanges();
          
                // Guardar la postulación en la base de datos, etc.

                // Redirigir a una página de confirmación u otra acción según tus necesidades
                return RedirectToAction("Index", "publicacion");
            }
        }
    }
