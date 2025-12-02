using MapaTriStackdb.Models;

public class ClienteViewModel
{
    public string Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }

    public IEnumerable<Equipamento> Equipamentos { get; set; }
    public IEnumerable<AlertaEquipamento> Alertas { get; set; }
    public IEnumerable<HistoricoEquipamento> Historicos { get; set; }
    public IEnumerable<MediaGeral> MediasGerais { get; set; }
}
