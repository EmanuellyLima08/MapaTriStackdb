using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MapaTriStackdb.Data;
using MapaTriStackdb.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace MapaTriStackdb.Controllers
{
    [Authorize]
    public class HistoricoEquipamentosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HistoricoEquipamentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔹 GET: HistoricoEquipamentos
        public async Task<IActionResult> Index()
        {
            // Pega o ID do usuário logado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Filtra apenas históricos de equipamentos do usuário logado
            var historicos = await _context.HistoricosEquipamentos
                .Include(h => h.Equipamento)
                .Where(h => h.Equipamento.UsuarioId == userId)
                .AsNoTracking()
                .ToListAsync();

            return View(historicos);
        }

        // 🔹 GET: HistoricoEquipamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var historico = await _context.HistoricosEquipamentos
                .Include(h => h.Equipamento)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.HistoricoEquipamentoId == id && m.Equipamento.UsuarioId == userId);

            if (historico == null)
                return NotFound();

            return View(historico);
        }

        // 🔹 GET: HistoricoEquipamentos/Create
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Mostra apenas equipamentos do usuário logado no dropdown
            ViewData["EquipamentoId"] = new SelectList(
                _context.Equipamentos.Where(e => e.UsuarioId == userId),
                "EquipamentoId",
                "Descricao"
            );

            return View();
        }

        // 🔹 POST: HistoricoEquipamentos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EquipamentoId,Descricao,Temperatura,Ar,Agua,Latitude,Longitude,Vento,Luz,Solo,DataLeitura")] HistoricoEquipamento historicoEquipamento)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Verifica se o equipamento pertence ao usuário
            var equipamento = await _context.Equipamentos.FirstOrDefaultAsync(e => e.EquipamentoId == historicoEquipamento.EquipamentoId && e.UsuarioId == userId);
            if (equipamento == null)
            {
                return Unauthorized(); // bloqueia tentativa indevida
            }

            if (ModelState.IsValid)
            {
                _context.Add(historicoEquipamento);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["EquipamentoId"] = new SelectList(
                _context.Equipamentos.Where(e => e.UsuarioId == userId),
                "EquipamentoId",
                "Descricao",
                historicoEquipamento.EquipamentoId
            );

            return View(historicoEquipamento);
        }

        // 🔹 GET: HistoricoEquipamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var historico = await _context.HistoricosEquipamentos
                .Include(h => h.Equipamento)
                .FirstOrDefaultAsync(h => h.HistoricoEquipamentoId == id && h.Equipamento.UsuarioId == userId);

            if (historico == null)
                return NotFound();

            ViewData["EquipamentoId"] = new SelectList(
                _context.Equipamentos.Where(e => e.UsuarioId == userId),
                "EquipamentoId",
                "Descricao",
                historico.EquipamentoId
            );

            return View(historico);
        }

        // 🔹 POST: HistoricoEquipamentos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HistoricoEquipamentoId,EquipamentoId,Descricao,Temperatura,Ar,Agua,Latitude,Longitude,Vento,Luz,Solo,DataLeitura")] HistoricoEquipamento historicoEquipamento)
        {
            if (id != historicoEquipamento.HistoricoEquipamentoId)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Confere se o equipamento pertence ao usuário
            var equipamento = await _context.Equipamentos.FirstOrDefaultAsync(e => e.EquipamentoId == historicoEquipamento.EquipamentoId && e.UsuarioId == userId);
            if (equipamento == null)
                return Unauthorized();

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
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["EquipamentoId"] = new SelectList(
                _context.Equipamentos.Where(e => e.UsuarioId == userId),
                "EquipamentoId",
                "Descricao",
                historicoEquipamento.EquipamentoId
            );

            return View(historicoEquipamento);
        }

        // 🔹 GET: HistoricoEquipamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var historico = await _context.HistoricosEquipamentos
                .Include(h => h.Equipamento)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.HistoricoEquipamentoId == id && m.Equipamento.UsuarioId == userId);

            if (historico == null)
                return NotFound();

            return View(historico);
        }

        // 🔹 POST: HistoricoEquipamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var historico = await _context.HistoricosEquipamentos
                .Include(h => h.Equipamento)
                .FirstOrDefaultAsync(h => h.HistoricoEquipamentoId == id && h.Equipamento.UsuarioId == userId);

            if (historico == null)
                return Unauthorized();

            _context.HistoricosEquipamentos.Remove(historico);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        private bool HistoricoEquipamentoExists(int id)
        {
            return _context.HistoricosEquipamentos.Any(e => e.HistoricoEquipamentoId == id);
        }
    }
}
