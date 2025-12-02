using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MapaTriStackdb.Models
{
    public class ConfigAlerta
    {
        [Key]
        public int ConfigAlertaId { get; set; }

        // Nome da configuração (ex: Enchente, Incêndio, Tempestade, Praga)
        [Required(ErrorMessage = "O nome da configuração é obrigatório.")]
        [StringLength(50)]
        [Display(Name = "Nome da Configuração")]
        public string Nome { get; set; } = string.Empty;

        // RELACIONAMENTO COM TIPO ALERTA
        [Required(ErrorMessage = "O tipo de alerta é obrigatório.")]
        [Display(Name = "Tipo de Alerta")]
        public int TipoAlertaId { get; set; }

        [ForeignKey(nameof(TipoAlertaId))]
        public TipoAlerta? TipoAlerta { get; set; }

        // RELACIONAMENTO COM CLIENTE (string, pois Identity usa string)
        [Required(ErrorMessage = "O cliente é obrigatório.")]
        [Display(Name = "Cliente")]
        public string ClienteId { get; set; } = string.Empty;

        [ForeignKey(nameof(ClienteId))]
        public Cliente? Cliente { get; set; }

        // ====== LIMITES QUE ACIONAM ALERTAS (CONFIGURÁVEIS POR CLIENTE) ======

        [Display(Name = "Temperatura Máxima (°C)")]
        public int? TemperaturaLimite { get; set; }

        [Display(Name = "Umidade do Ar Máxima (%)")]
        public int? ArLimite { get; set; }

        [Display(Name = "Velocidade Máxima do Vento (km/h)")]
        public int? VentoLimite { get; set; }

        [Display(Name = "Nível Máximo de Água (%)")]
        public int? AguaLimite { get; set; }

        [Display(Name = "Umidade Máxima do Solo (%)")]
        public int? SoloLimite { get; set; }

        // ====== NÍVEL DE ALERTA (para colorir: vermelho, amarelo, azul) ======
        [Required]
        [Display(Name = "Nível de Gravidade")]
        public string Nivel { get; set; } = "Leve";
        // valores possíveis: Leve, Moderado, Crítico
    }
}
