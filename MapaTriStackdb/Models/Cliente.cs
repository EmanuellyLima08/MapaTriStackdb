using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace MapaTriStackdb.Models
{
    public class Cliente
    {
        public string ClienteId { get; set; } = Guid.NewGuid().ToString();

        [Required]
        [StringLength(100)]
        public string Nome { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        // Relacionamentos — listas iniciadas para evitar erro de null
        public List<Equipamento> Equipamentos { get; set; } = new();
        public List<AlertaEquipamento> Alertas { get; set; } = new();
        public List<HistoricoEquipamento> Historicos { get; set; } = new();
        public List<MediaGeral> MediasGerais { get; set; } = new();
        public List<EquipamentoCliente> EquipamentosClientes { get; set; } = new();
    }
}
