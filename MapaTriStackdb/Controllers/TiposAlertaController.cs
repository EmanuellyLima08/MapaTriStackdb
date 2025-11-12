using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MapaTriStackdb.Data;
using MapaTriStackdb.Models;

namespace MapaTriStackdb.Controllers
{
    public class TiposAlertaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TiposAlertaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TiposAlerta
        public async Task<IActionResult> Index()
        {
            return View(await _context.TipoAlertas.ToListAsync());
        }

        // GET: TiposAlerta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoAlerta = await _context.TipoAlertas
                .FirstOrDefaultAsync(m => m.TipoAlertaId == id);
            if (tipoAlerta == null)
            {
                return NotFound();
            }

            return View(tipoAlerta);
        }

        // GET: TiposAlerta/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TiposAlerta/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoAlertaId,Descricao")] TipoAlerta tipoAlerta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoAlerta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoAlerta);
        }

        // GET: TiposAlerta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoAlerta = await _context.TipoAlertas.FindAsync(id);
            if (tipoAlerta == null)
            {
                return NotFound();
            }
            return View(tipoAlerta);
        }

        // POST: TiposAlerta/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoAlertaId,Descricao")] TipoAlerta tipoAlerta)
        {
            if (id != tipoAlerta.TipoAlertaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoAlerta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoAlertaExists(tipoAlerta.TipoAlertaId))
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
            return View(tipoAlerta);
        }

        // GET: TiposAlerta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tipoAlerta = await _context.TipoAlertas
                .FirstOrDefaultAsync(m => m.TipoAlertaId == id);
            if (tipoAlerta == null)
            {
                return NotFound();
            }

            return View(tipoAlerta);
        }

        // POST: TiposAlerta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tipoAlerta = await _context.TipoAlertas.FindAsync(id);
            if (tipoAlerta != null)
            {
                _context.TipoAlertas.Remove(tipoAlerta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoAlertaExists(int id)
        {
            return _context.TipoAlertas.Any(e => e.TipoAlertaId == id);
        }
    }
}
