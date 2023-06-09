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
    public class PublicacionesController : Controller
    {
        private readonly empleosDBContext _context;

        public PublicacionesController(empleosDBContext context)
        {
            _context = context;
        }

        // GET: Publicaciones
        public async Task<IActionResult> Index(int? id)
        {
            var idEmpresa = id; // Cambiar por el valor del IdEmpresa que deseas filtrar
            var empleosDBContext = _context.Publicaciones
                .Include(p => p.Empresa)
                .Where(p => p.Empresa.IdEmpresa == idEmpresa);

            return View(await empleosDBContext.ToListAsync());
        }

        // GET: Publicaciones/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Publicaciones == null)
            {
                return NotFound();
            }

            var publicacion = await _context.Publicaciones
                .Include(p => p.Empresa)
                .FirstOrDefaultAsync(m => m.IdPublicacion == id);
            if (publicacion == null)
            {
                return NotFound();
            }

            return View(publicacion);
        }

        // GET: Publicaciones/Create
        public IActionResult Create()
        {
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "IdEmpresa", "Email");
            return View();
        }

        // POST: Publicaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPublicacion,IdEmpresa,Titulo,Descripcion,FechaPublicacion,Salario,Experiencia,tipo_contrato,Localizacion")] Publicacion publicacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(publicacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "IdEmpresa", "Email", publicacion.IdEmpresa);
            return View(publicacion);
        }

        // GET: Publicaciones/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Publicaciones == null)
            {
                return NotFound();
            }

            var publicacion = await _context.Publicaciones.FindAsync(id);
            if (publicacion == null)
            {
                return NotFound();
            }
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "IdEmpresa", "Email", publicacion.IdEmpresa);
            return View(publicacion);
        }

        // POST: Publicaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPublicacion,IdEmpresa,Titulo,Descripcion,FechaPublicacion,Salario,Experiencia,tipo_contrato,Localizacion")] Publicacion publicacion)
        {
            if (id != publicacion.IdPublicacion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(publicacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PublicacionExists(publicacion.IdPublicacion))
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
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "IdEmpresa", "Email", publicacion.IdEmpresa);
            return View(publicacion);
        }

        // GET: Publicaciones/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Publicaciones == null)
            {
                return NotFound();
            }

            var publicacion = await _context.Publicaciones
                .Include(p => p.Empresa)
                .FirstOrDefaultAsync(m => m.IdPublicacion == id);
            if (publicacion == null)
            {
                return NotFound();
            }

            return View(publicacion);
        }

        // POST: Publicaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Publicaciones == null)
            {
                return Problem("Entity set 'empleosDBContext.Publicaciones'  is null.");
            }
            var publicacion = await _context.Publicaciones.FindAsync(id);
            if (publicacion != null)
            {
                _context.Publicaciones.Remove(publicacion);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublicacionExists(int id)
        {
          return (_context.Publicaciones?.Any(e => e.IdPublicacion == id)).GetValueOrDefault();
        }
    }
}
