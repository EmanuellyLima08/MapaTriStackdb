using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MapaTriStackdb.Data;
using MapaTriStackdb.Models;

namespace MapaTriStackdb.Controllers
{
    public class AlertasEquipamentoController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AlertasEquipamentoController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: AlertasEquipamento
        public async Task<IActionResult> Index()
        {
            // 🔹 Se quiser exibir apenas os alertas do usuário logado:
            var user = await _userManager.GetUserAsync(User);

            var alertas = await _context.AlertasEquipamentos
                .Include(a => a.Equipamento)
                .Include(a => a.TipoAlerta)
                .Include(a => a.Usuario)
                .Where(a => a.UsuarioId == user.Id)
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
                .Include(a => a.Usuario)
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
            return View();
        }

        // POST: AlertasEquipamento/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EquipamentoId,TipoAlertaId,Mensagem,DataAlerta")] AlertaEquipamento alertaEquipamento)
        {
            var user = await _userManager.GetUserAsync(User);
            alertaEquipamento.UsuarioId = user.Id;
            alertaEquipamento.DataAlerta = DateTime.Now;

            if (ModelState.IsValid)
            {
                _context.Add(alertaEquipamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["EquipamentoId"] = new SelectList(_context.Equipamentos, "EquipamentoId", "Descricao", alertaEquipamento.EquipamentoId);
            ViewData["TipoAlertaId"] = new SelectList(_context.TipoAlertas, "TipoAlertaId", "Descricao", alertaEquipamento.TipoAlertaId);
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
            return View(alertaEquipamento);
        }

        // POST: AlertasEquipamento/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AlertaEquipamentoId,EquipamentoId,TipoAlertaId,Mensagem,DataAlerta,UsuarioId")] AlertaEquipamento alertaEquipamento)
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
                .Include(a => a.Usuario)
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
