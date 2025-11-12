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
    public class MediasGeraisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MediasGeraisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MediasGerais
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MediasGerais.Include(m => m.Equipamento);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MediasGerais/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaGeral = await _context.MediasGerais
                .Include(m => m.Equipamento)
                .FirstOrDefaultAsync(m => m.MediaGeralId == id);
            if (mediaGeral == null)
            {
                return NotFound();
            }

            return View(mediaGeral);
        }

        // GET: MediasGerais/Create
        public IActionResult Create()
        {
            ViewData["EquipamentoId"] = new SelectList(_context.Equipamentos, "EquipamentoId", "Descricao");
            return View();
        }

        // POST: MediasGerais/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MediaGeralId,EquipamentoId,MediaTemperatura,MediaAr,MediaLuz,MediaAgua,MediaSolo,MediaVento")] MediaGeral mediaGeral)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mediaGeral);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipamentoId"] = new SelectList(_context.Equipamentos, "EquipamentoId", "Descricao", mediaGeral.EquipamentoId);
            return View(mediaGeral);
        }

        // GET: MediasGerais/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaGeral = await _context.MediasGerais.FindAsync(id);
            if (mediaGeral == null)
            {
                return NotFound();
            }
            ViewData["EquipamentoId"] = new SelectList(_context.Equipamentos, "EquipamentoId", "Descricao", mediaGeral.EquipamentoId);
            return View(mediaGeral);
        }

        // POST: MediasGerais/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MediaGeralId,EquipamentoId,MediaTemperatura,MediaAr,MediaLuz,MediaAgua,MediaSolo,MediaVento")] MediaGeral mediaGeral)
        {
            if (id != mediaGeral.MediaGeralId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mediaGeral);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MediaGeralExists(mediaGeral.MediaGeralId))
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
            ViewData["EquipamentoId"] = new SelectList(_context.Equipamentos, "EquipamentoId", "Descricao", mediaGeral.EquipamentoId);
            return View(mediaGeral);
        }

        // GET: MediasGerais/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mediaGeral = await _context.MediasGerais
                .Include(m => m.Equipamento)
                .FirstOrDefaultAsync(m => m.MediaGeralId == id);
            if (mediaGeral == null)
            {
                return NotFound();
            }

            return View(mediaGeral);
        }

        // POST: MediasGerais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mediaGeral = await _context.MediasGerais.FindAsync(id);
            if (mediaGeral != null)
            {
                _context.MediasGerais.Remove(mediaGeral);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MediaGeralExists(int id)
        {
            return _context.MediasGerais.Any(e => e.MediaGeralId == id);
        }
    }
}
