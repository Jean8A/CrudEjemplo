using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EJEMPLO_CRUD_SP.Models;

namespace EJEMPLO_CRUD_SP.Controllers
{
    public class InscripcionsController : Controller
    {
        private readonly UniversidadContext _context;

        public InscripcionsController(UniversidadContext context)
        {
            _context = context;
        }

        // GET: Inscripcions
        public async Task<IActionResult> Index()
        {
            var universidadContext = _context.Inscripcions.Include(i => i.IdEstudianteNavigation).Include(i => i.IdMateriaNavigation);
            return View(await universidadContext.ToListAsync());
        }

        // GET: Inscripcions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscripcion = await _context.Inscripcions
                .Include(i => i.IdEstudianteNavigation)
                .Include(i => i.IdMateriaNavigation)
                .FirstOrDefaultAsync(m => m.IdInscripcion == id);
            if (inscripcion == null)
            {
                return NotFound();
            }

            return View(inscripcion);
        }

        // GET: Inscripcions/Create
        public IActionResult Create()
        {
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiantes, "IdEstudiante", "IdEstudiante");
            ViewData["IdMateria"] = new SelectList(_context.Materias, "IdMateria", "IdMateria");
            return View();
        }

        // POST: Inscripcions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdInscripcion,IdEstudiante,IdMateria,FechaInscripcion")] Inscripcion inscripcion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inscripcion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiantes, "IdEstudiante", "IdEstudiante", inscripcion.IdEstudiante);
            ViewData["IdMateria"] = new SelectList(_context.Materias, "IdMateria", "IdMateria", inscripcion.IdMateria);
            return View(inscripcion);
        }

        // GET: Inscripcions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscripcion = await _context.Inscripcions.FindAsync(id);
            if (inscripcion == null)
            {
                return NotFound();
            }
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiantes, "IdEstudiante", "IdEstudiante", inscripcion.IdEstudiante);
            ViewData["IdMateria"] = new SelectList(_context.Materias, "IdMateria", "IdMateria", inscripcion.IdMateria);
            return View(inscripcion);
        }

        // POST: Inscripcions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdInscripcion,IdEstudiante,IdMateria,FechaInscripcion")] Inscripcion inscripcion)
        {
            if (id != inscripcion.IdInscripcion)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(inscripcion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InscripcionExists(inscripcion.IdInscripcion))
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
            ViewData["IdEstudiante"] = new SelectList(_context.Estudiantes, "IdEstudiante", "IdEstudiante", inscripcion.IdEstudiante);
            ViewData["IdMateria"] = new SelectList(_context.Materias, "IdMateria", "IdMateria", inscripcion.IdMateria);
            return View(inscripcion);
        }

        // GET: Inscripcions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var inscripcion = await _context.Inscripcions
                .Include(i => i.IdEstudianteNavigation)
                .Include(i => i.IdMateriaNavigation)
                .FirstOrDefaultAsync(m => m.IdInscripcion == id);
            if (inscripcion == null)
            {
                return NotFound();
            }

            return View(inscripcion);
        }

        // POST: Inscripcions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var inscripcion = await _context.Inscripcions.FindAsync(id);
            if (inscripcion != null)
            {
                _context.Inscripcions.Remove(inscripcion);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool InscripcionExists(int id)
        {
            return _context.Inscripcions.Any(e => e.IdInscripcion == id);
        }
    }
}
