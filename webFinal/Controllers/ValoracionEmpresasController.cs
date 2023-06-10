using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using webFinal.Models;

namespace webFinal.Controllers
{
    public class ValoracionEmpresasController : Controller
    {
        private readonly empleosDBContext _context;

        public ValoracionEmpresasController(empleosDBContext context)
        {
            _context = context;
        }

        // GET: ValoracionEmpresas
        public async Task<IActionResult> Index()
        {
            var empleosDBContext = _context.ValoracionesEmpresas.Include(v => v.Empresa).Include(v => v.Usuario);
            listados();
            return View(await empleosDBContext.ToListAsync());

        }

        public async Task<IActionResult> misValoraciones(int? id)
        {
            var idEmpresa = id; // Cambiar por el valor del IdEmpresa específico
            var empleosDBContext = _context.ValoracionesEmpresas
                .Include(v => v.Empresa)
                .Include(v => v.Usuario)
                .Where(v => v.Empresa.IdEmpresa == idEmpresa);

            return View(await empleosDBContext.ToListAsync());
        }


        // GET: ValoracionEmpresas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.ValoracionesEmpresas == null)
            {
                return NotFound();
            }

            var valoracionEmpresa = await _context.ValoracionesEmpresas
                .Include(v => v.Empresa)
                .Include(v => v.Usuario)
                .FirstOrDefaultAsync(m => m.IdValoracion == id);
            if (valoracionEmpresa == null)
            {
                return NotFound();
            }

            return View(valoracionEmpresa);
        }

        // GET: ValoracionEmpresas/Create
        [HttpGet]
        public ActionResult Create(int idEmpresa)
        {
            // Obtener los datos de la empresa correspondiente al ID
            Empresa empresa = _context.Empresas.FirstOrDefault(e => e.IdEmpresa == idEmpresa);

            if (empresa == null)
            {
                return NotFound(); // Manejar el caso en el que la empresa no exista
            }

            // Crear una instancia del modelo ValoracionEmpresa y asignar el ID de empresa
            ValoracionEmpresa valoracionEmpresa = new ValoracionEmpresa();
            valoracionEmpresa.IdEmpresa = empresa.IdEmpresa;

            // Pasar el nombre de la empresa a ViewBag para mostrarlo en la vista
            ViewBag.NombreEmpresa = empresa.Nombre;
            ViewBag.IdEmpresa = idEmpresa;
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Nombre");
            return View(valoracionEmpresa);
        }



        // POST: ValoracionEmpresas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Guardar(ValoracionEmpresa valoracionEmpresa)
        {
           
                _context.Add(valoracionEmpresa);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "ValoracionEmpresas");           

          
        }


        // GET: ValoracionEmpresas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.ValoracionesEmpresas == null)
            {
                return NotFound();
            }

            var valoracionEmpresa = await _context.ValoracionesEmpresas.FindAsync(id);
            if (valoracionEmpresa == null)
            {
                return NotFound();
            }
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "IdEmpresa", "Email", valoracionEmpresa.IdEmpresa);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", valoracionEmpresa.IdUsuario);
            return View(valoracionEmpresa);
        }

        // POST: ValoracionEmpresas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdValoracion,IdEmpresa,IdUsuario,Comentario,Calificacion,FechaValoracion")] ValoracionEmpresa valoracionEmpresa)
        {
            if (id != valoracionEmpresa.IdValoracion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(valoracionEmpresa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ValoracionEmpresaExists(valoracionEmpresa.IdValoracion))
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
            ViewData["IdEmpresa"] = new SelectList(_context.Empresas, "IdEmpresa", "Email", valoracionEmpresa.IdEmpresa);
            ViewData["IdUsuario"] = new SelectList(_context.Usuarios, "IdUsuario", "Apellido", valoracionEmpresa.IdUsuario);
            return View(valoracionEmpresa);
        }

        // GET: ValoracionEmpresas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.ValoracionesEmpresas == null)
            {
                return NotFound();
            }

            var valoracionEmpresa = await _context.ValoracionesEmpresas
                .Include(v => v.Empresa)
                .Include(v => v.Usuario)
                .FirstOrDefaultAsync(m => m.IdValoracion == id);
            if (valoracionEmpresa == null)
            {
                return NotFound();
            }

            return View(valoracionEmpresa);
        }

        // POST: ValoracionEmpresas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.ValoracionesEmpresas == null)
            {
                return Problem("Entity set 'empleosDBContext.ValoracionesEmpresas'  is null.");
            }
            var valoracionEmpresa = await _context.ValoracionesEmpresas.FindAsync(id);
            if (valoracionEmpresa != null)
            {
                _context.ValoracionesEmpresas.Remove(valoracionEmpresa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public void listados()
        {
            var lstEmpresa = new List<SelectListItem>();

            List<Empresa> empresas = _context.Empresas
                .Where(p => !string.IsNullOrEmpty(p.Nombre))
                .Select(p => new Empresa
                {
                    Nombre = p.Nombre,
                    IdEmpresa = p.IdEmpresa
                })
                .Distinct()
                .ToList();

            foreach (var empresa in empresas)
            {
                if (!empresa.Nombre.IsNullOrEmpty())
                {
                    var item = new SelectListItem
                    {
                        Text = empresa.Nombre,
                        Value = empresa.IdEmpresa.ToString()
                    };
                    lstEmpresa.Add(item);
                }
            }

            ViewData["listaEmpresa"] = lstEmpresa;

            // Asignar el ID de la primera empresa como seleccionada (opcional)
            if (empresas.Count > 0)
            {
                var firstEmpresa = empresas[0];
                ViewData["selectedEmpresaId"] = firstEmpresa.IdEmpresa.ToString();
            }
        }


        private bool ValoracionEmpresaExists(int id)
        {
            return (_context.ValoracionesEmpresas?.Any(e => e.IdValoracion == id)).GetValueOrDefault();
        }
    }
}