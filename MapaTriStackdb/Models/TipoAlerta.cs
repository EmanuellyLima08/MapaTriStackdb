using System.ComponentModel.DataAnnotations;

namespace MapaTriStackdb.Models
{
    public class TipoAlerta
    {
        public int TipoAlertaId { get; set; }


        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(100)]
        [Display(Name = "Tipo de Alerta")]
        public string? Descricao { get; set; }

        // Relacionamento
        public ICollection<AlertaEquipamento>? Alertas { get; set; }
    }
}
