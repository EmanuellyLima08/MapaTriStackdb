using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MapaTriStackdb.Models
{
    public class EquipamentoCliente
    {
        [Key]
        public int EquipamentoClienteId { get; set; }

        // 🔗 RELACIONAMENTO COM EQUIPAMENTO
        [Required(ErrorMessage = "O equipamento é obrigatório.")]
        [Display(Name = "Equipamento")]
        public int EquipamentoId { get; set; }

        [ForeignKey(nameof(EquipamentoId))]
        public Equipamento? Equipamento { get; set; }

        // 🔗 RELACIONAMENTO COM CLIENTE
        [Required(ErrorMessage = "Selecione um cliente.")]
        [Display(Name = "Cliente")]
        public string ClienteId { get; set; } = string.Empty;

        [ForeignKey(nameof(ClienteId))]
        public Cliente? Cliente { get; set; }

        // DATA DA COMPRA
        [Required(ErrorMessage = "A data da compra é obrigatória.")]
        [DataType(DataType.Date)]
        [Display(Name = "Data de Compra")]
        public DateTime DataCompra { get; set; }
    }
}
