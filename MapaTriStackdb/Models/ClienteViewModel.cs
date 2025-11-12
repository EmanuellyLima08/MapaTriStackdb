using System.Collections.Generic;

namespace MapaTriStackdb.Models
{
    public class ClienteViewModel
    {
        public string Id { get; set; }          // ID do usuário (do Identity)
        public string Nome { get; set; }        // Nome do usuário (UserName)
        public string Email { get; set; }       // E-mail do usuário

        // Relações com os outros dados
        public List<Equipamento> Equipamentos { get; set; }
        public List<AlertaEquipamento> Alertas { get; set; }
        public List<HistoricoEquipamento> Historicos { get; set; }
        public List<MediaGeral> MediasGerais { get; set; }
    }
}
