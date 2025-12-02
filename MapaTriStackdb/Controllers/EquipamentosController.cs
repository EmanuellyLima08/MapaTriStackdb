using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MapaTriStackdb.Data;
using MapaTriStackdb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MapaTriStackdb.Controllers
{
    [Authorize]
    public class EquipamentosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EquipamentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Equipamentos
        public async Task<IActionResult> Index()
        {
            // Inclui Cliente para exibir Nome na tabela
            var equipamentos = await _context.Equipamentos
                .Include(e => e.Cliente)
                .AsNoTracking()
                .ToListAsync();

            return View(equipamentos);
        }

        // GET: Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            // Inclui Cliente + coleções relacionadas
            var equipamento = await _context.Equipamentos
                .Include(e => e.Cliente)
                .Include(e => e.AlertasEquipamento)
                .Include(e => e.MediasGerais)
                .Include(e => e.Historicos)
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EquipamentoId == id);

            if (equipamento == null)
                return NotFound();

            return View(equipamento);
        }

        // GET: Create
        public IActionResult Create()
        {
            // DropDown exibindo Nome do Cliente
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nome");
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Equipamento equipamento)
        {
            if (!ModelState.IsValid)
            {
                ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nome", equipamento.ClienteId);
                return View(equipamento);
            }

            _context.Equipamentos.Add(equipamento);
            await _context.SaveChangesAsync();

            // Verifica alertas após criar
            await VerificarAlertas(equipamento);

            return RedirectToAction(nameof(Index));
        }

        // GET: Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var equipamento = await _context.Equipamentos
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EquipamentoId == id);

            if (equipamento == null)
                return NotFound();

            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nome", equipamento.ClienteId);

            return View(equipamento);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Equipamento equipamento)
        {
            if (id != equipamento.EquipamentoId)
                return NotFound();

            if (!ModelState.IsValid)
            {
                ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nome", equipamento.ClienteId);
                return View(equipamento);
            }

            _context.Update(equipamento);
            await _context.SaveChangesAsync();

            // Verifica alertas após editar
            await VerificarAlertas(equipamento);

            return RedirectToAction(nameof(Index));
        }

        // GET: Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var equipamento = await _context.Equipamentos
                .Include(e => e.Cliente) // Inclui Cliente para exibir Nome
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EquipamentoId == id);

            if (equipamento == null)
                return NotFound();

            return View(equipamento);
        }

        // POST: DeleteConfirmed
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipamento = await _context.Equipamentos.FindAsync(id);
            if (equipamento != null)
                _context.Equipamentos.Remove(equipamento);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // -------------------------------------------------------------
        // 🔥 MÉTODO PARA VERIFICAR ALERTAS (CÓPIA DA SUA REGRA DE NEGÓCIO)
        // -------------------------------------------------------------
        private async Task VerificarAlertas(Equipamento equipamento)
        {
            var configs = await _context.ConfigAlertas
                .Include(c => c.TipoAlerta)
                .AsNoTracking()
                .ToListAsync();

            foreach (var config in configs)
            {
                bool acendeu = false;

                if (config.Nome == "Enchente" && equipamento.Agua >= config.AguaLimite)
                    acendeu = true;

                if (config.Nome == "Incêndio" &&
                    (equipamento.Temperatura >= config.TemperaturaLimite ||
                     equipamento.Ar <= config.ArLimite))
                    acendeu = true;

                if (config.Nome == "Tempestade" &&
                    (equipamento.Vento >= config.VentoLimite ||
                     equipamento.Ar >= config.ArLimite))
                    acendeu = true;

                if (config.Nome == "Praga" && equipamento.Solo >= config.SoloLimite)
                    acendeu = true;

                if (acendeu)
                {
                    TempData["Alerta"] =
                        $"⚠ ALERTA DISPARADO! ({config.Nome}) — Tipo: {config.TipoAlerta?.Descricao ?? "Desconhecido"}";

                    return; // Mostra apenas o primeiro alerta por vez
                }
            }
        }
    }
}
