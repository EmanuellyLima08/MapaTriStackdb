using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MapaTriStackdb.Data;
using MapaTriStackdb.Models;
using Microsoft.AspNetCore.Authorization;

namespace MapaTriStackdb.Controllers
{
    [Authorize]
    public class TiposAlertaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public TiposAlertaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: TiposAlerta
        public async Task<IActionResult> Index()
        {
            var tipos = await _context.TipoAlertas
                .OrderBy(t => t.Descricao)
                .ToListAsync();

            return View(tipos);
        }

        // GET: TiposAlerta/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var tipoAlerta = await _context.TipoAlertas
                .FirstOrDefaultAsync(m => m.TipoAlertaId == id);

            if (tipoAlerta == null)
                return NotFound();

            return View(tipoAlerta);
        }

        // GET: TiposAlerta/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TiposAlerta/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TipoAlertaId,Descricao")] TipoAlerta tipoAlerta)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(tipoAlerta);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "✅ Tipo de alerta criado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {
                    TempData["ErrorMessage"] = "❌ Ocorreu um erro ao criar o tipo de alerta.";
                }
            }
            return View(tipoAlerta);
        }

        // GET: TiposAlerta/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var tipoAlerta = await _context.TipoAlertas.FindAsync(id);
            if (tipoAlerta == null)
                return NotFound();

            return View(tipoAlerta);
        }

        // POST: TiposAlerta/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TipoAlertaId,Descricao")] TipoAlerta tipoAlerta)
        {
            if (id != tipoAlerta.TipoAlertaId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoAlerta);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "✅ Tipo de alerta atualizado com sucesso!";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoAlertaExists(tipoAlerta.TipoAlertaId))
                        return NotFound();
                    else
                        throw;
                }
                catch (Exception)
                {
                    TempData["ErrorMessage"] = "❌ Ocorreu um erro ao atualizar o tipo de alerta.";
                }
            }
            return View(tipoAlerta);
        }

        // GET: TiposAlerta/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var tipoAlerta = await _context.TipoAlertas
                .FirstOrDefaultAsync(m => m.TipoAlertaId == id);

            if (tipoAlerta == null)
                return NotFound();

            return View(tipoAlerta);
        }

        // POST: TiposAlerta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var tipoAlerta = await _context.TipoAlertas.FindAsync(id);
                if (tipoAlerta != null)
                {
                    _context.TipoAlertas.Remove(tipoAlerta);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "🗑️ Tipo de alerta removido com sucesso!";
                }
                else
                {
                    TempData["ErrorMessage"] = "❌ Tipo de alerta não encontrado.";
                }
            }
            catch (Exception)
            {
                TempData["ErrorMessage"] = "⚠️ Não foi possível excluir o tipo de alerta. Ele pode estar vinculado a outros registros.";
            }

            return RedirectToAction(nameof(Index));
        }

        private bool TipoAlertaExists(int id)
        {
            return _context.TipoAlertas.Any(e => e.TipoAlertaId == id);
        }
    }
}
