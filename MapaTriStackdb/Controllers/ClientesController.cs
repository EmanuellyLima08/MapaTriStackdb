using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MapaTriStackdb.Data;
using MapaTriStackdb.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace MapaTriStackdb.Controllers
{
    public class ClientesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ClientesController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = _userManager.Users.ToList();
            var listaClientes = new List<ClienteViewModel>();

            foreach (var user in usuarios)
            {
                var equipamentos = await _context.Equipamentos
                    .Where(e => e.UsuarioId == user.Id)
                    .ToListAsync();

                var alertas = await _context.AlertasEquipamentos
                    .Where(a => a.UsuarioId == user.Id)
                    .ToListAsync();

                var historicos = await _context.HistoricosEquipamentos
                    .Where(h => h.UsuarioId == user.Id)
                    .ToListAsync();

                var medias = await _context.MediasGerais
                    .Where(m => m.UsuarioId == user.Id)
                    .ToListAsync();

                listaClientes.Add(new ClienteViewModel
                {
                    Id = user.Id,
                    Nome = user.UserName,
                    Email = user.Email,
                    Equipamentos = equipamentos,
                    Alertas = alertas,
                    Historicos = historicos,
                    MediasGerais = medias
                });
            }

            return View(listaClientes);
        }
    }
}
