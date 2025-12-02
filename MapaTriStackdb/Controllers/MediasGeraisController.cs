using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MapaTriStackdb.Data;
using MapaTriStackdb.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MapaTriStackdb.Controllers
{
    [Route("MediasGerais")]
    public class MediasGeraisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MediasGeraisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /MediasGerais ou /MediasGerais/Index
        [HttpGet("")]
        [HttpGet("Index")]
        public async Task<IActionResult> Index()
        {
            var medias = await _context.MediasGerais
                .Include(m => m.Equipamento)
                .Include(m => m.Cliente)
                .AsNoTracking()
                .ToListAsync();

            return View(medias);
        }

        // GET: /MediasGerais/Create
        [HttpGet("Create")]
        public async Task<IActionResult> Create()
        {
            var equipamentos = await _context.Equipamentos
                .Include(e => e.Cliente)
                .ToListAsync();

            ViewBag.Equipamentos = new SelectList(equipamentos, "EquipamentoId", "Descricao");
            return View();
        }

        // POST: /MediasGerais/Create
        [HttpPost("Create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int EquipamentoId)
        {
            // Busca o equipamento selecionado
            var equipamento = await _context.Equipamentos
                .Include(e => e.Cliente)
                .FirstOrDefaultAsync(e => e.EquipamentoId == EquipamentoId);

            if (equipamento == null)
            {
                TempData["ErrorMessage"] = "⚠️ Equipamento não encontrado!";
                return RedirectToAction(nameof(Create));
            }

            var clienteId = equipamento.ClienteId;

            // Busca todos os históricos do equipamento (sem filtrar pelo cliente)
            var historicos = await _context.HistoricosEquipamentos
                .Where(h => h.EquipamentoId == EquipamentoId)
                .ToListAsync();

            if (!historicos.Any())
            {
                TempData["ErrorMessage"] = "⚠️ Este equipamento ainda não possui registros válidos para calcular a média.";
                return RedirectToAction(nameof(Create));
            }

            // Calcula médias, ignorando nulos
            var media = new MediaGeral
            {
                EquipamentoId = EquipamentoId,
                ClienteId = clienteId,
                MediaTemperatura = historicos.Where(h => h.Temperatura.HasValue).Any() ?
                                    (int?)historicos.Where(h => h.Temperatura.HasValue).Average(h => h.Temperatura.Value) : null,
                MediaAr = historicos.Where(h => h.Ar.HasValue).Any() ?
                            (int?)historicos.Where(h => h.Ar.HasValue).Average(h => h.Ar.Value) : null,
                MediaAgua = historicos.Where(h => h.Agua.HasValue).Any() ?
                            (int?)historicos.Where(h => h.Agua.HasValue).Average(h => h.Agua.Value) : null,
                MediaSolo = historicos.Where(h => h.Solo.HasValue).Any() ?
                            (int?)historicos.Where(h => h.Solo.HasValue).Average(h => h.Solo.Value) : null,
                MediaVento = historicos.Where(h => h.Vento.HasValue).Any() ?
                            (int?)historicos.Where(h => h.Vento.HasValue).Average(h => h.Vento.Value) : null,
                MediaLuz = historicos.Where(h => h.Luz.HasValue).Any() ?
                            (int?)historicos.Where(h => h.Luz.HasValue).Average(h => h.Luz.Value) : null
            };

            _context.MediasGerais.Add(media);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "✅ Média calculada e salva com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        // GET: /MediasGerais/Details/5
        [HttpGet("Details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            var mediaGeral = await _context.MediasGerais
                .Include(m => m.Equipamento)
                .Include(m => m.Cliente)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.MediaGeralId == id);

            if (mediaGeral == null)
                return NotFound();

            return View(mediaGeral);
        }

        // GET: /MediasGerais/Delete/5
        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var mediaGeral = await _context.MediasGerais
                .Include(m => m.Equipamento)
                .Include(m => m.Cliente)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.MediaGeralId == id);

            if (mediaGeral == null)
                return NotFound();

            return View(mediaGeral);
        }

        // POST: /MediasGerais/Delete/5
        [HttpPost("Delete/{id}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mediaGeral = await _context.MediasGerais.FindAsync(id);
            if (mediaGeral != null)
            {
                _context.MediasGerais.Remove(mediaGeral);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
