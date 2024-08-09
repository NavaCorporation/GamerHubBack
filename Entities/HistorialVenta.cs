using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerHub_Backend.Entities
{
    public class HistorialVenta
    {
        public int Id { get; set; }
        public int IdOrden { get; set; }
        public OrdenCompra? OrdenCompra { get; set; }
        public int IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }
        [Column(TypeName = "Date")]
        public DateTime? FechaVenta { get; set; }
        [Required]
        public decimal? MontoTotal { get; set; }


    }
}
