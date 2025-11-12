using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MapaTriStackdb.Models
{
    public class EquipamentoCliente
    {
        public int EquipamentoClienteId { get; set; }

        [Required(ErrorMessage = "O equipamento é obrigatório.")]
        [Display(Name = "Equipamento")]
        public int EquipamentoId { get; set; }
        public Equipamento? Equipamento { get; set; }

        [Display(Name = "Usuário Responsável")]
        public string? UsuarioId { get; set; }
        public IdentityUser? Usuario { get; set; }

        [Required(ErrorMessage = "A data da compra é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "Informe uma data válida.")]
        [Display(Name = "Data de Compra")]
        public DateTime? DataCompra { get; set; }
    }
}
