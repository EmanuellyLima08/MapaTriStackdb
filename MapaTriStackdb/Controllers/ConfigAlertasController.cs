using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MapaTriStackdb.Data;
using MapaTriStackdb.Models;
using System.Collections.Generic;

namespace MapaTriStackdb.Controllers
{
    public class ConfigAlertasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ConfigAlertasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // LISTA TODAS AS CONFIGURAÇÕES
        public async Task<IActionResult> Index()
        {
            var configs = _context.ConfigAlertas
                .Include(c => c.TipoAlerta)
                .Include(c => c.Cliente);

            return View(await configs.ToListAsync());
        }

        // DETALHES
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var configAlerta = await _context.ConfigAlertas
                .Include(c => c.TipoAlerta)
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(m => m.ConfigAlertaId == id);

            if (configAlerta == null) return NotFound();

            return View(configAlerta);
        }

        // CREATE (GET)
        public IActionResult Create()
        {
            CarregarViewBags();

            return View();
        }

        // CREATE (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ConfigAlerta configAlerta)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(configAlerta);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "✅ Configuração de alerta criada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"❌ Erro ao criar configuração: {ex.Message}";
                }
            }

            CarregarViewBags(configAlerta);
            return View(configAlerta);
        }

        // EDIT (GET)
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();

            var configAlerta = await _context.ConfigAlertas.FindAsync(id);
            if (configAlerta == null) return NotFound();

            CarregarViewBags(configAlerta);

            return View(configAlerta);
        }

        // EDIT (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ConfigAlerta configAlerta)
        {
            if (id != configAlerta.ConfigAlertaId) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(configAlerta);
                    await _context.SaveChangesAsync();

                    TempData["SuccessMessage"] = "✅ Configuração atualizada com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ConfigAlertaExists(id)) return NotFound();
                    throw;
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"❌ Erro ao atualizar: {ex.Message}";
                }
            }

            CarregarViewBags(configAlerta);

            return View(configAlerta);
        }

        // DELETE (GET)
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();

            var configAlerta = await _context.ConfigAlertas
                .Include(c => c.TipoAlerta)
                .Include(c => c.Cliente)
                .FirstOrDefaultAsync(m => m.ConfigAlertaId == id);

            if (configAlerta == null) return NotFound();

            return View(configAlerta);
        }

        // DELETE (POST)
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var configAlerta = await _context.ConfigAlertas.FindAsync(id);

                if (configAlerta != null)
                {
                    _context.ConfigAlertas.Remove(configAlerta);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "🗑️ Configuração removida!";
                }
                else
                {
                    TempData["ErrorMessage"] = "❌ Configuração não encontrada.";
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"⚠️ Erro ao excluir: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        // CRIA ALERTAS PADRÃO
        public async Task<IActionResult> CriarAlertasPadrao()
        {
            if (_context.ConfigAlertas.Any())
            {
                TempData["ErrorMessage"] = "⚠️ Alertas padrão já existem.";
                return RedirectToAction(nameof(Index));
            }

            var cliente = await _context.Clientes.FirstOrDefaultAsync();
            if (cliente == null)
            {
                TempData["ErrorMessage"] = "⚠️ Nenhum cliente cadastrado.";
                return RedirectToAction(nameof(Index));
            }

            var tipos = await _context.TipoAlertas.ToListAsync();

            var padroes = new List<ConfigAlerta>();

            foreach (var tipo in tipos)
            {
                padroes.Add(new ConfigAlerta
                {
                    Nome = tipo.Descricao,
                    TipoAlertaId = tipo.TipoAlertaId,
                    ClienteId = cliente.ClienteId,
                    Nivel = "Moderado"
                });
            }

            _context.ConfigAlertas.AddRange(padroes);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "✅ Alertas padrão criados!";
            return RedirectToAction(nameof(Index));
        }

        // Carregar dropdowns
        private void CarregarViewBags(ConfigAlerta? c = null)
        {
            ViewData["TipoAlertaId"] = new SelectList(
                _context.TipoAlertas,
                "TipoAlertaId",
                "Descricao",
                c?.TipoAlertaId);

            ViewData["ClienteId"] = new SelectList(
                _context.Clientes,
                "ClienteId",
                "Nome",
                c?.ClienteId);

            ViewData["Nivel"] = new SelectList(
                new[] { "Leve", "Moderado", "Crítico" },
                c?.Nivel);
        }

        private bool ConfigAlertaExists(int id)
        {
            return _context.ConfigAlertas.Any(e => e.ConfigAlertaId == id);
        }
    }
}
