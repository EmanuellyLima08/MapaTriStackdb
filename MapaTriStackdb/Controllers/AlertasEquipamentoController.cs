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
    public class AlertasEquipamentoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AlertasEquipamentoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AlertasEquipamento
        public async Task<IActionResult> Index()
        {
            var alertas = await _context.AlertasEquipamentos
                .Include(a => a.Equipamento)
                .Include(a => a.TipoAlerta)
                .Include(a => a.Cliente)
                .ToListAsync();

            return View(alertas);
        }

        // GET: AlertasEquipamento/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var alertaEquipamento = await _context.AlertasEquipamentos
                .Include(a => a.Equipamento)
                .Include(a => a.TipoAlerta)
                .Include(a => a.Cliente)
                .FirstOrDefaultAsync(m => m.AlertaEquipamentoId == id);

            if (alertaEquipamento == null)
                return NotFound();

            return View(alertaEquipamento);
        }

        // GET: AlertasEquipamento/Create
        public IActionResult Create()
        {
            ViewData["EquipamentoId"] = new SelectList(_context.Equipamentos, "EquipamentoId", "Descricao");
            ViewData["TipoAlertaId"] = new SelectList(_context.TipoAlertas, "TipoAlertaId", "Descricao");
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nome");

            return View();
        }

        // POST: AlertasEquipamento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EquipamentoId,TipoAlertaId,Mensagem,DataAlerta,ClienteId")] AlertaEquipamento alertaEquipamento)
        {
            alertaEquipamento.DataAlerta = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Add(alertaEquipamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["EquipamentoId"] = new SelectList(_context.Equipamentos, "EquipamentoId", "Descricao", alertaEquipamento.EquipamentoId);
            ViewData["TipoAlertaId"] = new SelectList(_context.TipoAlertas, "TipoAlertaId", "Descricao", alertaEquipamento.TipoAlertaId);
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nome", alertaEquipamento.ClienteId);

            return View(alertaEquipamento);
        }

        // GET: AlertasEquipamento/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var alertaEquipamento = await _context.AlertasEquipamentos.FindAsync(id);
            if (alertaEquipamento == null)
                return NotFound();

            ViewData["EquipamentoId"] = new SelectList(_context.Equipamentos, "EquipamentoId", "Descricao", alertaEquipamento.EquipamentoId);
            ViewData["TipoAlertaId"] = new SelectList(_context.TipoAlertas, "TipoAlertaId", "Descricao", alertaEquipamento.TipoAlertaId);
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nome", alertaEquipamento.ClienteId);

            return View(alertaEquipamento);
        }

        // POST: AlertasEquipamento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AlertaEquipamentoId,EquipamentoId,TipoAlertaId,Mensagem,DataAlerta,ClienteId")] AlertaEquipamento alertaEquipamento)
        {
            if (id != alertaEquipamento.AlertaEquipamentoId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alertaEquipamento);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AlertaEquipamentoExists(alertaEquipamento.AlertaEquipamentoId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["EquipamentoId"] = new SelectList(_context.Equipamentos, "EquipamentoId", "Descricao", alertaEquipamento.EquipamentoId);
            ViewData["TipoAlertaId"] = new SelectList(_context.TipoAlertas, "TipoAlertaId", "Descricao", alertaEquipamento.TipoAlertaId);
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nome", alertaEquipamento.ClienteId);

            return View(alertaEquipamento);
        }

        // GET: AlertasEquipamento/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var alertaEquipamento = await _context.AlertasEquipamentos
                .Include(a => a.Equipamento)
                .Include(a => a.TipoAlerta)
                .Include(a => a.Cliente)
                .FirstOrDefaultAsync(m => m.AlertaEquipamentoId == id);

            if (alertaEquipamento == null)
                return NotFound();

            return View(alertaEquipamento);
        }

        // POST: AlertasEquipamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var alertaEquipamento = await _context.AlertasEquipamentos.FindAsync(id);
            if (alertaEquipamento != null)
                _context.AlertasEquipamentos.Remove(alertaEquipamento);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlertaEquipamentoExists(int id)
        {
            return _context.AlertasEquipamentos.Any(e => e.AlertaEquipamentoId == id);
        }
    }
}
