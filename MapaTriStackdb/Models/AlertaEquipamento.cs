using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MapaTriStackdb.Models
{
    public class AlertaEquipamento
    {
        [Key]
        public int AlertaEquipamentoId { get; set; }

        [Required(ErrorMessage = "O Equipamento é obrigatório.")]
        [Display(Name = "Equipamento")]
        public int EquipamentoId { get; set; }

        [ForeignKey(nameof(EquipamentoId))]
        public Equipamento Equipamento { get; set; } = null!; // 🔹 Corrigido

        [Required(ErrorMessage = "O Tipo de Alerta é obrigatório.")]
        [Display(Name = "Tipo de Alerta")]
        public int TipoAlertaId { get; set; }

        [ForeignKey(nameof(TipoAlertaId))]
        public TipoAlerta TipoAlerta { get; set; } = null!; // 🔹 Corrigido

        [Required(ErrorMessage = "A Mensagem do Alerta é obrigatória.")]
        [StringLength(200)]
        [Display(Name = "Mensagem do Alerta")]
        public string Mensagem { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        [Display(Name = "Data do Alerta")]
        public DateOnly? DataAlerta { get; set; }
    }
}
