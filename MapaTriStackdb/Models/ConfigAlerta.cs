using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MapaTriStackdb.Models
{
    public class ConfigAlerta
    {
        public int ConfigAlertaId { get; set; }

        [Required(ErrorMessage = "O nome da configuração é obrigatório.")]
        [StringLength(50)]
        [Display(Name = "Nome da Configuração")]
        public string? Nome { get; set; }

        [Display(Name = "Temperatura Máxima (°C)")]
        public int? Temperatura { get; set; }

        [Display(Name = "Umidade do Ar Máxima (%)")]
        public int? Ar { get; set; }

        [Display(Name = "Velocidade Máxima do Vento (km/h)")]
        public int? Vento { get; set; }

        [Display(Name = "Nível Máximo de Água (%)")]
        public int? Agua { get; set; }

        [Display(Name = "Umidade Máxima do Solo (%)")]
        public int? Solo { get; set; }
    }
}
