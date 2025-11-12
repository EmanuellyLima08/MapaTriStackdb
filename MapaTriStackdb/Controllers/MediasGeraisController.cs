using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MapaTriStackdb.Data;
using MapaTriStackdb.Models;
using System.Security.Claims;
using System.Linq;
using System.Threading.Tasks;

namespace MapaTriStackdb.Controllers
{
    public class MediasGeraisController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MediasGeraisController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /MediasGerais
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var medias = await _context.MediasGerais
                .Include(m => m.Equipamento)
                .Where(m => m.Equipamento.UsuarioId == userId)
                .ToListAsync();

            return View(medias);
        }

        // GET: /MediasGerais/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var mediaGeral = await _context.MediasGerais
                .Include(m => m.Equipamento)
                .FirstOrDefaultAsync(m => m.MediaGeralId == id && m.Equipamento.UsuarioId == userId);

            if (mediaGeral == null)
                return NotFound();

            return View(mediaGeral);
        }

        // GET: /MediasGerais/Create
        public async Task<IActionResult> Create()
        {
            // Carregar equipamentos do usuário para dropdown, se necessário
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Equipamentos = await _context.Equipamentos
                .Where(e => e.UsuarioId == userId)
                .ToListAsync();

            return View();
        }

        // POST: /MediasGerais/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MediaGeral mediaGeral)
        {
            if (ModelState.IsValid)
            {
                _context.MediasGerais.Add(mediaGeral);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Recarregar equipamentos caso o ModelState seja inválido
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Equipamentos = await _context.Equipamentos
                .Where(e => e.UsuarioId == userId)
                .ToListAsync();

            return View(mediaGeral);
        }

        // GET: /MediasGerais/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var mediaGeral = await _context.MediasGerais.FindAsync(id);
            if (mediaGeral == null)
                return NotFound();

            // Carregar equipamentos do usuário para dropdown
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Equipamentos = await _context.Equipamentos
                .Where(e => e.UsuarioId == userId)
                .ToListAsync();

            return View(mediaGeral);
        }

        // POST: /MediasGerais/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, MediaGeral mediaGeral)
        {
            if (id != mediaGeral.MediaGeralId)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mediaGeral);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.MediasGerais.Any(e => e.MediaGeralId == id))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            // Recarregar equipamentos caso o ModelState seja inválido
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewBag.Equipamentos = await _context.Equipamentos
                .Where(e => e.UsuarioId == userId)
                .ToListAsync();

            return View(mediaGeral);
        }

        // GET: /MediasGerais/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var mediaGeral = await _context.MediasGerais
                .Include(m => m.Equipamento)
                .FirstOrDefaultAsync(m => m.MediaGeralId == id);

            if (mediaGeral == null)
                return NotFound();

            return View(mediaGeral);
        }

        // POST: /MediasGerais/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mediaGeral = await _context.MediasGerais.FindAsync(id);
            _context.MediasGerais.Remove(mediaGeral);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
