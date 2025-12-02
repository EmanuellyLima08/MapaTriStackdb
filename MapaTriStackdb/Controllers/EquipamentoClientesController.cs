using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MapaTriStackdb.Data;
using MapaTriStackdb.Models;
using Microsoft.AspNetCore.Authorization;

namespace MapaTriStackdb.Controllers
{
    [Authorize]
    public class EquipamentoClientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EquipamentoClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // 🔹 Lista todos os equipamentos vinculados a clientes
        public async Task<IActionResult> Index()
        {
            var dados = await _context.EquipamentosClientes
                .Include(e => e.Equipamento)
                .Include(e => e.Cliente) // Inclui Cliente para exibir Nome
                .AsNoTracking()
                .ToListAsync();

            return View(dados);
        }

        // 🔹 Detalhes de um vínculo específico
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var item = await _context.EquipamentosClientes
                .Include(e => e.Equipamento)
                .Include(e => e.Cliente) // Inclui Cliente para exibir Nome
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.EquipamentoClienteId == id);

            if (item == null) return NotFound();

            return View(item);
        }

        // 🔹 Página de criação
        public IActionResult Create()
        {
            ViewData["EquipamentoId"] = new SelectList(_context.Equipamentos, "EquipamentoId", "Descricao");
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nome"); // Dropdown com Nome
            return View();
        }

        // 🔹 Criação de um novo vínculo
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EquipamentoCliente equipamentoCliente)
        {
            // Verifica se equipamento existe
            if (!_context.Equipamentos.Any(e => e.EquipamentoId == equipamentoCliente.EquipamentoId))
                ModelState.AddModelError("EquipamentoId", "Equipamento não encontrado.");

            // Verifica se cliente existe
            if (!_context.Clientes.Any(u => u.ClienteId == equipamentoCliente.ClienteId))
                ModelState.AddModelError("ClienteId", "Cliente não encontrado.");

            if (!ModelState.IsValid)
            {
                ViewData["EquipamentoId"] = new SelectList(_context.Equipamentos, "EquipamentoId", "Descricao", equipamentoCliente.EquipamentoId);
                ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nome", equipamentoCliente.ClienteId);
                return View(equipamentoCliente);
            }

            try
            {
                _context.Add(equipamentoCliente);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "✅ Vínculo de equipamento e cliente criado com sucesso!";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "❌ Ocorreu um erro ao criar o vínculo.";
            }

            return RedirectToAction(nameof(Index));
        }

        // 🔹 Página de edição
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var item = await _context.EquipamentosClientes
                .Include(e => e.Cliente) // Inclui Cliente para exibir Nome no dropdown
                .FirstOrDefaultAsync(e => e.EquipamentoClienteId == id);

            if (item == null) return NotFound();

            ViewData["EquipamentoId"] = new SelectList(_context.Equipamentos, "EquipamentoId", "Descricao", item.EquipamentoId);
            ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nome", item.ClienteId);
            return View(item);
        }

        // 🔹 Edição de um vínculo existente
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EquipamentoCliente equipamentoCliente)
        {
            if (id != equipamentoCliente.EquipamentoClienteId) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewData["EquipamentoId"] = new SelectList(_context.Equipamentos, "EquipamentoId", "Descricao", equipamentoCliente.EquipamentoId);
                ViewData["ClienteId"] = new SelectList(_context.Clientes, "ClienteId", "Nome", equipamentoCliente.ClienteId);
                return View(equipamentoCliente);
            }

            try
            {
                _context.Update(equipamentoCliente);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "✅ Vínculo atualizado com sucesso!";
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "❌ Ocorreu um erro ao atualizar o vínculo.";
            }

            return RedirectToAction(nameof(Index));
        }

        // 🔹 Página de confirmação de exclusão
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var item = await _context.EquipamentosClientes
                .Include(e => e.Equipamento)
                .Include(e => e.Cliente) // Inclui Cliente para exibir Nome
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.EquipamentoClienteId == id);

            if (item == null) return NotFound();

            return View(item);
        }

        // 🔹 Exclusão de um vínculo
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var item = await _context.EquipamentosClientes.FindAsync(id);

            if (item != null)
            {
                try
                {
                    _context.EquipamentosClientes.Remove(item);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "🗑️ Vínculo removido com sucesso!";
                }
                catch (Exception)
                {
                    TempData["ErrorMessage"] = "⚠️ Não foi possível remover o vínculo.";
                }
            }

            return RedirectToAction(nameof(Index));
        }

        // 🔹 Verifica se o vínculo existe
        private bool EquipamentoClienteExists(int id)
        {
            return _context.EquipamentosClientes.Any(e => e.EquipamentoClienteId == id);
        }
    }
}
