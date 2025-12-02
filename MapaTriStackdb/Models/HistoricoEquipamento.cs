using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MapaTriStackdb.Models
{
    public class HistoricoEquipamento
    {
        [Key]
        public int HistoricoEquipamentoId { get; set; }

        // 🔗 RELACIONAMENTO COM EQUIPAMENTO
        [Required(ErrorMessage = "O equipamento é obrigatório.")]
        [Display(Name = "Equipamento")]
        public int EquipamentoId { get; set; }

        [ForeignKey(nameof(EquipamentoId))]
        public Equipamento? Equipamento { get; set; }

        // DESCRIÇÃO DA LEITURA
        [StringLength(200)]
        [Display(Name = "Descrição da Leitura")]
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

        // DATA DA LEITURA
        [Required(ErrorMessage = "A data da leitura é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "Informe uma data válida.")]
        [Display(Name = "Data da Leitura")]
        public DateTime? DataLeitura { get; set; }

        // 🔗 RELACIONAMENTO COM CLIENTE
        [Required(ErrorMessage = "Selecione um cliente.")]
        [Display(Name = "Cliente")]
        public string ClienteId { get; set; } = string.Empty;

        [ForeignKey(nameof(ClienteId))]
        public Cliente? Cliente { get; set; }
    }
}
