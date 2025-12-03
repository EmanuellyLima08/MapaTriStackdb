using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MapaTriStackdb.Data;
using MapaTriStackdb.Models;

namespace MapaTriStackdb.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ClientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Clientes
        public async Task<IActionResult> Index()
        {
            var clientes = await _context.Clientes
                .Include(c => c.Equipamentos)
                .Include(c => c.Alertas)
                .Include(c => c.Historicos)
                .Include(c => c.MediasGerais)
                .AsNoTracking()
                .ToListAsync();

            return View(clientes);
        }

        // GET: Details
        public async Task<IActionResult> Details(int id)
        {
            var cliente = await _context.Clientes
                .Include(c => c.Equipamentos)
                .Include(c => c.Alertas)
                .Include(c => c.Historicos)
                .Include(c => c.MediasGerais)
                .FirstOrDefaultAsync(c => c.ClienteId == id);

            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        // GET: Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Nome,Email")] Cliente cliente)
        {
            if (!ModelState.IsValid)
                return View(cliente);

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // GET: Edit
        public async Task<IActionResult> Edit(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClienteId,Nome,Email")] Cliente cliente)
        {
            if (id != cliente.ClienteId)
                return NotFound();

            if (!ModelState.IsValid)
                return View(cliente);

            try
            {
                _context.Update(cliente);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Clientes.Any(c => c.ClienteId == id))
                    return NotFound();
                else
                    throw;
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Delete
        public async Task<IActionResult> Delete(int id)
        {
            var cliente = await _context.Clientes
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.ClienteId == id);

            if (cliente == null)
                return NotFound();

            return View(cliente);
        }

        // POST: Delete Confirmed
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
