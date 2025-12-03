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

        // -------------------------------------------------------------
        // 🔥 MÉTODO PARA REGISTRAR NO HISTÓRICO AUTOMATICAMENTE
        // -------------------------------------------------------------
        private async Task RegistrarHistoricoAsync(Equipamento equipamento)
        {
            var historico = new HistoricoEquipamento
            {
                EquipamentoId = equipamento.EquipamentoId,
                Temperatura = equipamento.Temperatura,
                Ar = equipamento.Ar,
                Agua = equipamento.Agua,
                Latitude = equipamento.Latitude,
                Longitude = equipamento.Longitude,
                Vento = equipamento.Vento,
                Luz = equipamento.Luz,
                Solo = equipamento.Solo,
                DataLeitura = DateTime.Now,
                ClienteId = equipamento.ClienteId
            };

            _context.HistoricosEquipamentos.Add(historico);
            await _context.SaveChangesAsync();
        }

        // -------------------------------------------------------------
        // 🔥 MÉTODO PARA ATUALIZAR MÉDIA GERAL
        // -------------------------------------------------------------
        private async Task AtualizarMediaGeralAsync(Equipamento equipamento)
        {
            var historicos = await _context.HistoricosEquipamentos
                .Where(h => h.EquipamentoId == equipamento.EquipamentoId)
                .ToListAsync();

            if (!historicos.Any())
                return;

            // Calcula médias
            int? mediaTemp = (int?)historicos.Average(h => h.Temperatura ?? 0);
            int? mediaAr = (int?)historicos.Average(h => h.Ar ?? 0);
            int? mediaAgua = (int?)historicos.Average(h => h.Agua ?? 0);
            int? mediaVento = (int?)historicos.Average(h => h.Vento ?? 0);
            int? mediaSolo = (int?)historicos.Average(h => h.Solo ?? 0);
            int? mediaLuz = (int?)historicos.Average(h => h.Luz ?? 0);

            // Verifica se já existe registro de média para esse equipamento e cliente
            var media = await _context.MediasGerais
                .FirstOrDefaultAsync(m => m.EquipamentoId == equipamento.EquipamentoId &&
                                          m.ClienteId == equipamento.ClienteId);

            if (media == null)
            {
                // Cria novo registro de média
                media = new MediaGeral
                {
                    EquipamentoId = equipamento.EquipamentoId,
                    ClienteId = equipamento.ClienteId,
                    MediaTemperatura = mediaTemp,
                    MediaAr = mediaAr,
                    MediaAgua = mediaAgua,
                    MediaVento = mediaVento,
                    MediaSolo = mediaSolo,
                    MediaLuz = mediaLuz
                };
                _context.MediasGerais.Add(media);
            }
            else
            {
                // Atualiza média existente
                media.MediaTemperatura = mediaTemp;
                media.MediaAr = mediaAr;
                media.MediaAgua = mediaAgua;
                media.MediaVento = mediaVento;
                media.MediaSolo = mediaSolo;
                media.MediaLuz = mediaLuz;

                _context.MediasGerais.Update(media);
            }

            await _context.SaveChangesAsync();
        }

        // -------------------------------------------------------------
        // GET: Equipamentos
        // -------------------------------------------------------------
        public async Task<IActionResult> Index()
        {
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

            // Salva equipamento
            _context.Equipamentos.Add(equipamento);
            await _context.SaveChangesAsync();

            // Salva no histórico automaticamente
            await RegistrarHistoricoAsync(equipamento);

            // Atualiza média geral
            await AtualizarMediaGeralAsync(equipamento);

            // Verifica alertas
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

            // Atualiza equipamento
            _context.Update(equipamento);
            await _context.SaveChangesAsync();

            // Salva no histórico automaticamente
            await RegistrarHistoricoAsync(equipamento);

            // Atualiza média geral
            await AtualizarMediaGeralAsync(equipamento);

            // Verifica alertas
            await VerificarAlertas(equipamento);

            return RedirectToAction(nameof(Index));
        }

        // GET: Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var equipamento = await _context.Equipamentos
                .Include(e => e.Cliente)
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
        // 🔥 MÉTODO PARA VERIFICAR ALERTAS
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

                    return;
                }
            }
        }
    }
}
