using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MapaTriStackdb.Data;
using MapaTriStackdb.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace MapaTriStackdb.Controllers
{
    [Authorize]
    public class EquipamentosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public EquipamentosController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Equipamentos
        public async Task<IActionResult> Index()
        {
            // Obtém o ID do usuário logado
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Filtra apenas os equipamentos do usuário logado
            var equipamentos = await _context.Equipamentos
                .Where(e => e.UsuarioId == userId)
                .AsNoTracking()
                .ToListAsync();

            return View(equipamentos);
        }

        // GET: Equipamentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var equipamento = await _context.Equipamentos
                .Include(e => e.AlertasEquipamento)
                .Include(e => e.MediasGerais)
                .Include(e => e.EquipamentosClientes)
                .Include(e => e.Historicos)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.EquipamentoId == id && m.UsuarioId == userId);

            if (equipamento == null)
                return NotFound();

            return View(equipamento);
        }

        // GET: Equipamentos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Equipamentos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Descricao,Temperatura,Ar,Agua,Latitude,Longitude,Vento,Luz,Solo")] Equipamento equipamento)
        {
            if (!ModelState.IsValid)
                return View(equipamento);

            // Define o usuário logado como dono do equipamento
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            equipamento.UsuarioId = userId;

            _context.Equipamentos.Add(equipamento);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Equipamentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var equipamento = await _context.Equipamentos
                .FirstOrDefaultAsync(e => e.EquipamentoId == id && e.UsuarioId == userId);

            if (equipamento == null)
                return NotFound();

            return View(equipamento);
        }

        // POST: Equipamentos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EquipamentoId,Descricao,Temperatura,Ar,Agua,Latitude,Longitude,Vento,Luz,Solo")] Equipamento equipamento)
        {
            if (id != equipamento.EquipamentoId)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var equipamentoExistente = await _context.Equipamentos
                .FirstOrDefaultAsync(e => e.EquipamentoId == id && e.UsuarioId == userId);

            if (equipamentoExistente == null)
                return NotFound();

            if (!ModelState.IsValid)
                return View(equipamento);

            // Mantém o vínculo com o usuário
            equipamento.UsuarioId = userId;

            try
            {
                _context.Entry(equipamentoExistente).CurrentValues.SetValues(equipamento);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EquipamentoExists(equipamento.EquipamentoId))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Equipamentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var equipamento = await _context.Equipamentos
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.EquipamentoId == id && m.UsuarioId == userId);

            if (equipamento == null)
                return NotFound();

            return View(equipamento);
        }

        // POST: Equipamentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var equipamento = await _context.Equipamentos
                .FirstOrDefaultAsync(e => e.EquipamentoId == id && e.UsuarioId == userId);

            if (equipamento != null)
            {
                _context.Equipamentos.Remove(equipamento);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }

        private bool EquipamentoExists(int id)
        {
            return _context.Equipamentos.Any(e => e.EquipamentoId == id);
        }
    }
}
