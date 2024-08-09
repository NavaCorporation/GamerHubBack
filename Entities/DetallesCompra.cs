using Microsoft.Build.Framework;

namespace GamerHub_Backend.Entities
{
    public class DetallesCompra
    {
        public int Id { get; set; }
        [Required]
        public int IdOrden { get; set; }
        public OrdenCompra? OrdenCompra { get; set; }
        [Required]
        public int IdProducto { get; set; }
        public Producto? Producto { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [Required]
        public decimal Precio { get; set; }


    }
}
