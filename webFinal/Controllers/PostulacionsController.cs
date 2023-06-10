using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using webFinal.Models;

namespace webFinal.Controllers
{
    public class PostulacionsController : Controller
    {
        private readonly empleosDBContext _context;

        public PostulacionsController(empleosDBContext context)
        {
            _context = context;
        }

        // GET: Postulacions
        public async Task<IActionResult> Index()
        {
            var empleosDBContext = _context.postulaciones.Include(p => p.Empresa).Include(p => p.Publicacion).Include(p => p.Usuario);
            return View(await empleosDBContext.ToListAsync());
        }

        public async Task<IActionResult> mispostulaciones(int? id)
        {
            var idUsuario = id;
            var empleosDBContext = _context.postulaciones
                .Include(p => p.Empresa)
                .Include(p => p.Publicacion)
                .Include(p => p.Usuario)
                .Where(p => p.Usuario.IdUsuario == idUsuario);

            return View(await empleosDBContext.ToListAsync());
        }
        public async Task<IActionResult> ver(int? id)
        {
            var idUsuario = id;
            var empleosDBContext = _context.postulaciones
                .Include(p => p.Empresa)
                .Include(p => p.Publicacion)
                .Include(p => p.Usuario)
                .Where(p => p.Empresa.IdEmpresa == idUsuario);

            return View(await empleosDBContext.ToListAsync());
        }


        // GET: Postulacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.postulaciones == null)
            {
                return NotFound();
            }

            var postulacion = await _context.postulaciones
                .Include(p => p.Empresa)
                .Include(p => p.Publicacion)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.IdPostulacion == id);
            if (postulacion == null)
            {
                return NotFound();
            }

            return View(postulacion);
        }

        public IActionResult Consultar()
        {
            // Obtener todas las postulaciones y cargar los datos relacionados
            var postulaciones = _context.postulaciones
                .Include(p => p.Usuario)
                .Include(p => p.Empresa)
                .Include(p => p.Publicacion)
                .ToList();

            // Pasar las postulaciones a la vista
            return View(postulaciones);
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

            _context.postulaciones.Add(postulacion);
            _context.SaveChanges();

            // Guardar la postulación en la base de datos, etc.

            // Redirigir a una página de confirmación u otra acción según tus necesidades
            return RedirectToAction("Index", "publicacion");
        }


        /// <summary>
        /// ////////////////
        /// </summary>
        /// <returns></returns>


        // GET: Postulacions/Create
        public IActionResult Create()
        {
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "IdEmpresa", "Email");
            ViewData["IdPublicacion"] = new SelectList(_context.Publicaciones, "IdPublicacion", "Titulo");
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido");
            return View();
        }

        // POST: Postulacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPostulacion,IdUsuario,IdEmpresa,IdPublicacion,FechaPostulacion")] Postulacion postulacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(postulacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "IdEmpresa", "Email", postulacion.IdEmpresa);
            ViewData["IdPublicacion"] = new SelectList(_context.Publicaciones, "IdPublicacion", "Titulo", postulacion.IdPublicacion);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", postulacion.IdUsuario);
            return View(postulacion);
        }

        // GET: Postulacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.postulaciones == null)
            {
                return NotFound();
            }

            var postulacion = await _context.postulaciones.FindAsync(id);
            if (postulacion == null)
            {
                return NotFound();
            }
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "IdEmpresa", "Email", postulacion.IdEmpresa);
            ViewData["IdPublicacion"] = new SelectList(_context.Publicaciones, "IdPublicacion", "Titulo", postulacion.IdPublicacion);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", postulacion.IdUsuario);
            return View(postulacion);
        }

        // POST: Postulacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPostulacion,IdUsuario,IdEmpresa,IdPublicacion,FechaPostulacion")] Postulacion postulacion)
        {
            if (id != postulacion.IdPostulacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(postulacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PostulacionExists(postulacion.IdPostulacion))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "IdEmpresa", "Email", postulacion.IdEmpresa);
            ViewData["IdPublicacion"] = new SelectList(_context.Publicaciones, "IdPublicacion", "Titulo", postulacion.IdPublicacion);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", postulacion.IdUsuario);
            return View(postulacion);
        }

        // GET: Postulacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.postulaciones == null)
            {
                return NotFound();
            }

            var postulacion = await _context.postulaciones
                .Include(p => p.Empresa)
                .Include(p => p.Publicacion)
                .Include(p => p.Usuario)
                .FirstOrDefaultAsync(m => m.IdPostulacion == id);
            if (postulacion == null)
            {
                return NotFound();
            }

            return View(postulacion);
        }

        // POST: Postulacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.postulaciones == null)
            {
                return Problem("Entity set 'empleosDBContext.postulaciones'  is null.");
            }
            var postulacion = await _context.postulaciones.FindAsync(id);
            if (postulacion != null)
            {
                _context.postulaciones.Remove(postulacion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PostulacionExists(int id)
        {
            return (_context.postulaciones?.Any(e => e.IdPostulacion == id)).GetValueOrDefault();
        }
    }
}