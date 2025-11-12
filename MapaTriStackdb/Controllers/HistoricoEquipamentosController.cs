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
    public class HistoricoEquipamentosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HistoricoEquipamentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: HistoricoEquipamentos
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.HistoricosEquipamentos.Include(h => h.Equipamento);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: HistoricoEquipamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historicoEquipamento = await _context.HistoricosEquipamentos
                .Include(h => h.Equipamento)
                .FirstOrDefaultAsync(m => m.HistoricoEquipamentoId == id);
            if (historicoEquipamento == null)
            {
                return NotFound();
            }

            return View(historicoEquipamento);
        }

        // GET: HistoricoEquipamentos/Create
        public IActionResult Create()
        {
            ViewData["EquipamentoId"] = new SelectList(_context.Equipamentos, "EquipamentoId", "Descricao");
            return View();
        }

        // POST: HistoricoEquipamentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HistoricoEquipamentoId,EquipamentoId,Descricao,Temperatura,Ar,Agua,Latitude,Longitude,Vento,Luz,Solo,DataLeitura")] HistoricoEquipamento historicoEquipamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historicoEquipamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipamentoId"] = new SelectList(_context.Equipamentos, "EquipamentoId", "Descricao", historicoEquipamento.EquipamentoId);
            return View(historicoEquipamento);
        }

        // GET: HistoricoEquipamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historicoEquipamento = await _context.HistoricosEquipamentos.FindAsync(id);
            if (historicoEquipamento == null)
            {
                return NotFound();
            }
            ViewData["EquipamentoId"] = new SelectList(_context.Equipamentos, "EquipamentoId", "Descricao", historicoEquipamento.EquipamentoId);
            return View(historicoEquipamento);
        }

        // POST: HistoricoEquipamentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HistoricoEquipamentoId,EquipamentoId,Descricao,Temperatura,Ar,Agua,Latitude,Longitude,Vento,Luz,Solo,DataLeitura")] HistoricoEquipamento historicoEquipamento)
        {
            if (id != historicoEquipamento.HistoricoEquipamentoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historicoEquipamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HistoricoEquipamentoExists(historicoEquipamento.HistoricoEquipamentoId))
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
            ViewData["EquipamentoId"] = new SelectList(_context.Equipamentos, "EquipamentoId", "Descricao", historicoEquipamento.EquipamentoId);
            return View(historicoEquipamento);
        }

        // GET: HistoricoEquipamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var historicoEquipamento = await _context.HistoricosEquipamentos
                .Include(h => h.Equipamento)
                .FirstOrDefaultAsync(m => m.HistoricoEquipamentoId == id);
            if (historicoEquipamento == null)
            {
                return NotFound();
            }

            return View(historicoEquipamento);
        }

        // POST: HistoricoEquipamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historicoEquipamento = await _context.HistoricosEquipamentos.FindAsync(id);
            if (historicoEquipamento != null)
            {
                _context.HistoricosEquipamentos.Remove(historicoEquipamento);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HistoricoEquipamentoExists(int id)
        {
            return _context.HistoricosEquipamentos.Any(e => e.HistoricoEquipamentoId == id);
        }
    }
}
