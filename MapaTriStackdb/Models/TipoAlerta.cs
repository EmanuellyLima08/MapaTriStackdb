using System.ComponentModel.DataAnnotations;

namespace MapaTriStackdb.Models
{
    public class TipoAlerta
    {
        [Key]
        public int TipoAlertaId { get; set; }

        // 🔹 Descrição do tipo de alerta (Leve, Moderado, Crítico)
        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(100)]
        [Display(Name = "Tipo de Alerta")]
        public string Descricao { get; set; } = string.Empty;

        // 🔹 Relacionamento com os alertas gerados
        public ICollection<AlertaEquipamento> Alertas { get; set; } = new List<AlertaEquipamento>();
    }
}
