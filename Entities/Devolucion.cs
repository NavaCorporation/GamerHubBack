using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace GamerHub_Backend.Entities
{
    public class Devolucion
    {
        public int Id { get; set; }
        public int IdOreden { get; set; }
        public OrdenCompra OrdenCompra { get; set; } = null!;
        public int IdProducto { get; set; }
        public Producto Producto { get; set; } = null!;
        [Column(TypeName = "Date")]
        public DateTime FechaDevolucion { get; set; }
        [Required]
        public string? Razon { get; set; }
        [Required]
        public string Estado { get; set; } = null!;
    }
}
