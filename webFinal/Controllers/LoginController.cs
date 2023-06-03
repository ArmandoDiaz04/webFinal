    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using webFinal.Models;

    namespace webFinal.Controllers
    {
        public class LoginController : Controller
        {
            private readonly empleosDBContext _dbContext;

            public LoginController(empleosDBContext dbContext)
            {
                _dbContext = dbContext;
            }

          
            [HttpPost]
            public IActionResult Index(string Email, string Password)
            {
                var user = _dbContext.Usuarios.FirstOrDefault(u => u.Email == Email && u.contrasenia == Password);

                if (user != null)
                {
                    // Guardar estado de inicio de sesión en una variable de sesión
                    HttpContext.Session.SetString("UserId", user.IdUsuario.ToString());
                    HttpContext.Session.SetString("RolId", user.idrol.ToString());

                // Redirigir a la página de inicio después de iniciar sesión
                return RedirectToAction("Index", "Home");
                }

                // Usuario no válido, muestra un mensaje de error
                ViewData["Error"] = "Credenciales inválidas";
                return View();
            }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); // Elimina todas las variables de sesión

            return RedirectToAction("Index", "Home");
        }
        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("UserId") != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

    }
}
