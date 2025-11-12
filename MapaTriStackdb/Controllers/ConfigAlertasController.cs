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
    public class ConfigAlertasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConfigAlertasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ConfigAlertas
        public async Task<IActionResult> Index()
        {
            return View(await _context.ConfigAlertas.ToListAsync());
        }

        // GET: ConfigAlertas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configAlerta = await _context.ConfigAlertas
                .FirstOrDefaultAsync(m => m.ConfigAlertaId == id);
            if (configAlerta == null)
            {
                return NotFound();
            }

            return View(configAlerta);
        }

        // GET: ConfigAlertas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ConfigAlertas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ConfigAlertaId,Nome,Temperatura,Ar,Vento,Agua,Solo")] ConfigAlerta configAlerta)
        {
            if (ModelState.IsValid)
            {
                _context.Add(configAlerta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(configAlerta);
        }

        // GET: ConfigAlertas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configAlerta = await _context.ConfigAlertas.FindAsync(id);
            if (configAlerta == null)
            {
                return NotFound();
            }
            return View(configAlerta);
        }

        // POST: ConfigAlertas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ConfigAlertaId,Nome,Temperatura,Ar,Vento,Agua,Solo")] ConfigAlerta configAlerta)
        {
            if (id != configAlerta.ConfigAlertaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(configAlerta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfigAlertaExists(configAlerta.ConfigAlertaId))
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
            return View(configAlerta);
        }

        // GET: ConfigAlertas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var configAlerta = await _context.ConfigAlertas
                .FirstOrDefaultAsync(m => m.ConfigAlertaId == id);
            if (configAlerta == null)
            {
                return NotFound();
            }

            return View(configAlerta);
        }

        // POST: ConfigAlertas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var configAlerta = await _context.ConfigAlertas.FindAsync(id);
            if (configAlerta != null)
            {
                _context.ConfigAlertas.Remove(configAlerta);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ConfigAlertaExists(int id)
        {
            return _context.ConfigAlertas.Any(e => e.ConfigAlertaId == id);
        }
    }
}
