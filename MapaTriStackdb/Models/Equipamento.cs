using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace MapaTriStackdb.Models
{
    public class Equipamento
    {
        [Key]
        public int EquipamentoId { get; set; }

        [Required(ErrorMessage = "A descrição do equipamento é obrigatória.")]
        [StringLength(100)]
        [Display(Name = "Descrição do Equipamento")]
        public string? Descricao { get; set; }

        [Display(Name = "Temperatura (°C)")]
        public int? Temperatura { get; set; }

        [Display(Name = "Umidade do Ar (%)")]
        public int? Ar { get; set; }

        [Display(Name = "Nível de Água (%)")]
        public int? Agua { get; set; }

        [Display(Name = "Latitude")]
        public int? Latitude { get; set; }

        [Display(Name = "Longitude")]
        public int? Longitude { get; set; }

        [Display(Name = "Velocidade do Vento (km/h)")]
        public int? Vento { get; set; }

        [Display(Name = "Luminosidade (lux)")]
        public int? Luz { get; set; }

        [Display(Name = "Umidade do Solo (%)")]
        public int? Solo { get; set; }

        // 🔗 CLIENTE DIRETO DO EQUIPAMENTO (FK)
        [Required(ErrorMessage = "Selecione um cliente.")]
        [Display(Name = "Cliente")]
        public string ClienteId { get; set; }  // ID do Cliente (string)

        // Navegação
        [ForeignKey(nameof(ClienteId))]
        public Cliente? Cliente { get; set; }

        // 🔗 RELACIONAMENTOS
        public ICollection<AlertaEquipamento>? AlertasEquipamento { get; set; }
        public ICollection<MediaGeral>? MediasGerais { get; set; }
        public ICollection<EquipamentoCliente>? EquipamentosClientes { get; set; }
        public ICollection<HistoricoEquipamento>? Historicos { get; set; }
    }
}
