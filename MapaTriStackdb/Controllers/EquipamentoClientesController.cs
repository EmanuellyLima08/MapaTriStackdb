using System;
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
    public class EquipamentoClientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EquipamentoClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: EquipamentoClientes
        public async Task<IActionResult> Index()
        {
            var equipamentosClientes = _context.EquipamentosClientes
                .Include(e => e.Equipamento)
                .Include(e => e.Usuario);

            return View(await equipamentosClientes.ToListAsync());
        }

        // GET: EquipamentoClientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var equipamentoCliente = await _context.EquipamentosClientes
                .Include(e => e.Equipamento)
                .Include(e => e.Usuario)
                .FirstOrDefaultAsync(m => m.EquipamentoClienteId == id);

            if (equipamentoCliente == null)
                return NotFound();

            return View(equipamentoCliente);
        }

        // GET: EquipamentoClientes/Create
        public IActionResult Create()
        {
            ViewData["EquipamentoId"] = new SelectList(_context.Equipamentos, "EquipamentoId", "Descricao");
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "UserName");
            return View();
        }

        // POST: EquipamentoClientes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EquipamentoClienteId,EquipamentoId,UsuarioId,DataCompra")] EquipamentoCliente equipamentoCliente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipamentoCliente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["EquipamentoId"] = new SelectList(_context.Equipamentos, "EquipamentoId", "Descricao", equipamentoCliente.EquipamentoId);
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "UserName", equipamentoCliente.UsuarioId);
            return View(equipamentoCliente);
        }

        // GET: EquipamentoClientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var equipamentoCliente = await _context.EquipamentosClientes.FindAsync(id);
            if (equipamentoCliente == null)
                return NotFound();

            ViewData["EquipamentoId"] = new SelectList(_context.Equipamentos, "EquipamentoId", "Descricao", equipamentoCliente.EquipamentoId);
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "UserName", equipamentoCliente.UsuarioId);
            return View(equipamentoCliente);
        }

        // POST: EquipamentoClientes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EquipamentoClienteId,EquipamentoId,UsuarioId,DataCompra")] EquipamentoCliente equipamentoCliente)
        {
            if (id != equipamentoCliente.EquipamentoClienteId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(equipamentoCliente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EquipamentoClienteExists(equipamentoCliente.EquipamentoClienteId))
                        return NotFound();
                    else
                        throw;
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["EquipamentoId"] = new SelectList(_context.Equipamentos, "EquipamentoId", "Descricao", equipamentoCliente.EquipamentoId);
            ViewData["UsuarioId"] = new SelectList(_context.Users, "Id", "UserName", equipamentoCliente.UsuarioId);
            return View(equipamentoCliente);
        }

        // GET: EquipamentoClientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var equipamentoCliente = await _context.EquipamentosClientes
                .Include(e => e.Equipamento)
                .Include(e => e.Usuario)
                .FirstOrDefaultAsync(m => m.EquipamentoClienteId == id);

            if (equipamentoCliente == null)
                return NotFound();

            return View(equipamentoCliente);
        }

        // POST: EquipamentoClientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var equipamentoCliente = await _context.EquipamentosClientes.FindAsync(id);
            if (equipamentoCliente != null)
                _context.EquipamentosClientes.Remove(equipamentoCliente);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EquipamentoClienteExists(int id)
        {
            return _context.EquipamentosClientes.Any(e => e.EquipamentoClienteId == id);
        }
    }
}
