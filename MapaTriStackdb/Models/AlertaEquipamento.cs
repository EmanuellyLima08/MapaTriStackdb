using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MapaTriStackdb.Models
{
    public class AlertaEquipamento
    {
        [Key]
        public int AlertaEquipamentoId { get; set; }

        // Equipamento vinculado ao alerta
        [Required(ErrorMessage = "O Equipamento é obrigatório.")]
        [Display(Name = "Equipamento")]
        public int EquipamentoId { get; set; }

        [ForeignKey(nameof(EquipamentoId))]
        public Equipamento Equipamento { get; set; } = null!;

        // Tipo do alerta
        [Required(ErrorMessage = "O Tipo de Alerta é obrigatório.")]
        [Display(Name = "Tipo de Alerta")]
        public int TipoAlertaId { get; set; }

        [ForeignKey(nameof(TipoAlertaId))]
        public TipoAlerta TipoAlerta { get; set; } = null!;

        // Mensagem
        [Required(ErrorMessage = "A Mensagem do Alerta é obrigatória.")]
        [StringLength(200)]
        [Display(Name = "Mensagem do Alerta")]
        public string Mensagem { get; set; } = string.Empty;

        // Data e hora
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Data e Hora do Alerta")]
        public DateTime DataAlerta { get; set; } = DateTime.Now;

        // Cliente associado ao alerta (IdentityUser → string)
        [Required]
        [Display(Name = "Cliente")]
        public string ClienteId { get; set; } = string.Empty;

        [ForeignKey(nameof(ClienteId))]
        public Cliente Cliente { get; set; } = null!;
    }
}
