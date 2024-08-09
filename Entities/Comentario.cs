using Microsoft.Build.Framework;

namespace GamerHub_Backend.Entities
{
    public class Comentario
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public Usuario? Usuario { get; set; }
        public int IdProducto { get; set; }
        public Producto? Producto { get; set; }
        [Required]
        public string? ComentarioUsuario { get; set; }
    }
}
