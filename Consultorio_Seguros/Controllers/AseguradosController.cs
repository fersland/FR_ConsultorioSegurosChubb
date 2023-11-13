using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Consultorio_Seguros.Data;
using Consultorio_Seguros.Models;

namespace Consultorio_Seguros.Controllers
{
    public class AseguradosController : Controller
    {
        private readonly AppDbContext _context;

        public AseguradosController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Asegurados.Include(a => a.Clientes).Include(a => a.Seguros);
            return View(await appDbContext.ToListAsync());
        }

        public IActionResult Inicio(string searchBy, string search)
        {
            if (searchBy == "Cedula")
            {
                var appCedula = _context.Asegurados.Include(a => a.Clientes).Include(a => a.Seguros).Where(x => x.Clientes.Cedula == search || search == null);
                return View(appCedula.ToList());
                //return View(_context.Asegurados.Where(x => x.Clientes.Cedula == search || search == null).ToList());
            }
            else if (searchBy == "Codigo")
            {
                var appCodigo = _context.Asegurados.Include(a => a.Clientes).Include(a => a.Seguros).Where(x => x.Seguros.Codigo == search || search == null);
                return View(appCodigo.ToList());
            }
            else
            {
                var appDbContext = _context.Asegurados.Include(a => a.Clientes).Include(a => a.Seguros);
                return View(appDbContext.ToList());
            }
        }

            // GET: Asegurados/Details/5
            public async Task<IActionResult> Details(int? id)
            {
                if (id == null || _context.Asegurados == null)
                {
                    return NotFound();
                }

                var asegurado = await _context.Asegurados
                    .Include(a => a.Clientes)
                    .Include(a => a.Seguros)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (asegurado == null)
                {
                    return NotFound();
                }

                return View(asegurado);
            }

            // GET: Asegurados/Create
            public IActionResult Create()
            {
                ViewBag.Seguros = _context.Seguros.ToList();
                ViewBag.Clientes = _context.Clientes.ToList();
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Create(Asegurado asegurado)
            {
                try
                {
                    var existe = _context.Asegurados.Any(a => a.ClienteId == asegurado.ClienteId && a.SeguroId == asegurado.SeguroId);

                    if (!existe)
                    {
                        _context.Asegurados.Add(asegurado);
                        _context.SaveChanges();
                        TempData["successMessage"] = "Seguro agregado correctamente.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["errorMessage"] = "Este Seguro ya existe.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception)
                {
                    TempData["errorMessage"] = "Los datos son invalidos";
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }


            public IActionResult Edit(int? id)
            {
                if (id == null || _context.Asegurados == null)
                {
                    return NotFound();
                }

                var asegurado = _context.Asegurados.Find(id);
                if (asegurado == null)
                {
                    return NotFound();
                }
                ViewBag.Seguros = _context.Seguros.ToList();
                ViewBag.Clientes = _context.Clientes.ToList();
                return View(asegurado);
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public IActionResult Edit(int id, Asegurado asegurado)
            {

                try
                {
                    var existe = _context.Asegurados.Any(a => a.ClienteId == asegurado.ClienteId && a.SeguroId == asegurado.SeguroId);

                    if (!existe)
                    {
                        _context.Asegurados.Update(asegurado);
                        _context.SaveChanges();
                        TempData["successMessage"] = "Seguro actualizado correctamente.";
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        TempData["errorMessage"] = "Este Seguro ya existe.";
                        return RedirectToAction(nameof(Index));
                    }
                }
                catch (Exception)
                {
                    TempData["errorMessage"] = "Los datos son invalidos";
                    return RedirectToAction(nameof(Index));
                }
                return View();
            }

            
                // GET: Asegurados/Delete/5
            public async Task<IActionResult> Delete(int? id)
            {
                if (id == null || _context.Asegurados == null)
                {
                    return NotFound();
                }

                var asegurado = await _context.Asegurados
                    .Include(a => a.Clientes)
                    .Include(a => a.Seguros)
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (asegurado == null)
                {
                    return NotFound();
                }

                return View(asegurado);
            }

        // POST: Asegurados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Asegurados == null)
            {
                return Problem("Entity set 'AppDbContext.Asegurados'  is null.");
            }
            var asegurado = await _context.Asegurados.FindAsync(id);
            if (asegurado != null)
            {
                _context.Asegurados.Remove(asegurado);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AseguradoExists(int id)
        {
          return (_context.Asegurados?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
