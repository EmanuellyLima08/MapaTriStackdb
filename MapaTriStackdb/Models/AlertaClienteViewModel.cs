namespace MapaTriStackdb.Models
{
    public class AlertaClienteViewModel
    {
        public string ClienteNome { get; set; }
        public string AlertaNome { get; set; } // Ex: Enchente, Incêndio
        public string TipoAlerta { get; set; } // Crítico, Moderado, Leve
        public string Cor => TipoAlerta switch
        {
            "Crítico" => "red",
            "Moderado" => "yellow",
            "Leve" => "blue",
            _ => "gray"
        };
    }
}