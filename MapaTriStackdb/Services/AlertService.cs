using MapaTriStackdb.Data;
using MapaTriStackdb.Models;
using Microsoft.EntityFrameworkCore;

namespace MapaTriStackdb.Services
{
    public class AlertService
    {
        private readonly ApplicationDbContext _context;

        public AlertService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Método para verificar alertas de todos os clientes
        public async Task<List<AlertaClienteViewModel>> VerificarAlertasAsync()
        {
            var clientes = await _context.Clientes
                .Include(c => c.Equipamentos)
                    .ThenInclude(e => e.Historicos) // Dados dos sensores
                .ToListAsync();

            var configs = await _context.ConfigAlertas
                .Include(c => c.TipoAlerta)
                .ToListAsync();

            var alertas = new List<AlertaClienteViewModel>();

            foreach (var cliente in clientes)
            {
                foreach (var equipamento in cliente.Equipamentos)
                {
                    foreach (var historico in equipamento.Historicos)
                    {
                        foreach (var config in configs)
                        {
                            bool acendeu = false;

                            // 🔥 VERIFICAÇÃO ATUALIZADA COM OS NOVOS NOMES 🔥

                            if (config.Nome == "Enchente" &&
                                historico.Agua >= config.AguaLimite)
                                acendeu = true;

                            if (config.Nome == "Incêndio" &&
                               (historico.Temperatura >= config.TemperaturaLimite ||
                                historico.Ar <= config.ArLimite))
                                acendeu = true;

                            if (config.Nome == "Tempestade" &&
                               (historico.Vento >= config.VentoLimite ||
                                historico.Ar >= config.ArLimite))
                                acendeu = true;

                            if (config.Nome == "Praga" &&
                                historico.Solo >= config.SoloLimite)
                                acendeu = true;

                            // Se algum limite foi ultrapassado → cria alerta
                            if (acendeu)
                            {
                                alertas.Add(new AlertaClienteViewModel
                                {
                                    ClienteNome = cliente.Nome,
                                    AlertaNome = config.Nome,
                                    TipoAlerta = config.TipoAlerta?.Descricao ?? "Desconhecido"
                                });
                            }
                        }
                    }
                }
            }

            return alertas;
        }
    }
}
