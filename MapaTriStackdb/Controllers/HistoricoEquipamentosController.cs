using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MapaTriStackdb.Data;
using MapaTriStackdb.Models;
using Microsoft.AspNetCore.Authorization;

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

        // GET: HistoricoEquipamentos
        public async Task<IActionResult> Index()
        {
            var historicos = await _context.HistoricosEquipamentos
                .Include(h => h.Equipamento)
                .Include(h => h.Cliente)
                .AsNoTracking()
                .ToListAsync();

            return View(historicos);
        }

        // GET: Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var historico = await _context.HistoricosEquipamentos
                .Include(h => h.Equipamento)
                .Include(h => h.Cliente)
                .AsNoTracking()
                .FirstOrDefaultAsync(h => h.HistoricoEquipamentoId == id);

            if (historico == null)
                return NotFound();

            return View(historico);
        }

        // GET: Create
        public IActionResult Create()
        {
            ViewData["EquipamentoId"] = new SelectList(_context.Equipamentos, "EquipamentoId", "Descricao");
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nome");
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EquipamentoId,ClienteId,Descricao,Temperatura,Ar,Agua,Latitude,Longitude,Vento,Luz,Solo,DataLeitura")] HistoricoEquipamento historicoEquipamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(historicoEquipamento);
                await _context.SaveChangesAsync();

                await VerificarAlertas(historicoEquipamento);

                return RedirectToAction(nameof(Index));
            }

            ViewData["EquipamentoId"] = new SelectList(_context.Equipamentos, "EquipamentoId", "Descricao", historicoEquipamento.EquipamentoId);
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nome", historicoEquipamento.ClienteId);
            return View(historicoEquipamento);
        }

        // GET: Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var historico = await _context.HistoricosEquipamentos.FindAsync(id);
            if (historico == null) return NotFound();

            ViewData["EquipamentoId"] = new SelectList(_context.Equipamentos, "EquipamentoId", "Descricao", historico.EquipamentoId);
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nome", historico.ClienteId);
            return View(historico);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HistoricoEquipamentoId,EquipamentoId,ClienteId,Descricao,Temperatura,Ar,Agua,Latitude,Longitude,Vento,Luz,Solo,DataLeitura")] HistoricoEquipamento historicoEquipamento)
        {
            if (id != historicoEquipamento.HistoricoEquipamentoId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(historicoEquipamento);
                    await _context.SaveChangesAsync();

                    await VerificarAlertas(historicoEquipamento);
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

            ViewData["EquipamentoId"] = new SelectList(_context.Equipamentos, "EquipamentoId", "Descricao", historicoEquipamento.EquipamentoId);
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nome", historicoEquipamento.ClienteId);
            return View(historicoEquipamento);
        }

        // GET: Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var historico = await _context.HistoricosEquipamentos
                .Include(h => h.Equipamento)
                .Include(h => h.Cliente)
                .AsNoTracking()
                .FirstOrDefaultAsync(h => h.HistoricoEquipamentoId == id);

            if (historico == null) return NotFound();

            return View(historico);
        }

        // POST: DeleteConfirmed
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var historico = await _context.HistoricosEquipamentos.FindAsync(id);
            if (historico != null)
            {
                _context.HistoricosEquipamentos.Remove(historico);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        // 🔹 Checa se o registro existe
        private bool HistoricoEquipamentoExists(int id)
        {
            return _context.HistoricosEquipamentos.Any(e => e.HistoricoEquipamentoId == id);
        }

        // 🔹 Método para verificar alertas
        private async Task VerificarAlertas(HistoricoEquipamento historico)
        {
            var config = await _context.ConfigAlertas.FirstOrDefaultAsync();
            if (config == null) return;

            bool alerta = false;
            string mensagem = "";

            if (historico.Temperatura >= config.TemperaturaLimite)
            {
                alerta = true;
                mensagem += "Temperatura acima do limite! ";
            }

            if (historico.Ar >= config.ArLimite)
            {
                alerta = true;
                mensagem += "Qualidade do ar acima do limite! ";
            }

            if (historico.Agua >= config.AguaLimite)
            {
                alerta = true;
                mensagem += "Nível de água acima do permitido! ";
            }

            if (alerta)
                TempData["Alerta"] = mensagem;
        }
    }
}
