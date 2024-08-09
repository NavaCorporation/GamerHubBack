using Microsoft.Build.Framework;
using System.Text.Json.Serialization;

namespace GamerHub_Backend.Entities
{
    public class Categoria
    {
        public int Id { get; set; }
        [Required]
        public string NombreCategoria { get; set; } = null!;

        public string Descripcion { get; set; } = null!;

        [JsonIgnore]
        public List<Producto>? Productos { get; set; }

    }
}
