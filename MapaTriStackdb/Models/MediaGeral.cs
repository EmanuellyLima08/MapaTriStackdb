using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MapaTriStackdb.Models
{
    public class MediaGeral
    {
        [Key]
        public int MediaGeralId { get; set; }

        // 🔗 RELACIONAMENTO COM EQUIPAMENTO
        [Required(ErrorMessage = "O Equipamento é obrigatório.")]
        [Display(Name = "Equipamento")]
        public int EquipamentoId { get; set; }

        [ForeignKey(nameof(EquipamentoId))]
        public Equipamento Equipamento { get; set; } = null!;

        // MÉDIAS DOS DADOS
        [Display(Name = "Média de Temperatura")]
        public int? MediaTemperatura { get; set; }

        [Display(Name = "Média de Umidade do Ar")]
        public int? MediaAr { get; set; }

        [Display(Name = "Média de Luminosidade")]
        public int? MediaLuz { get; set; }

        [Display(Name = "Média de Água")]
        public int? MediaAgua { get; set; }

        [Display(Name = "Média de Umidade do Solo")]
        public int? MediaSolo { get; set; }

        [Display(Name = "Média de Vento")]
        public int? MediaVento { get; set; }

        // 🔗 RELACIONAMENTO COM CLIENTE
        [Required(ErrorMessage = "Selecione um cliente.")]
        [Display(Name = "Cliente")]
        public int ClienteId { get; set; }

        [ForeignKey(nameof(ClienteId))]
        public Cliente Cliente { get; set; } = null!;
    }
}
