using Microsoft.Build.Framework;
using System.Text.Json.Serialization;

namespace GamerHub_Backend.Entities
{
    public class Rol
    {
        public int Id { get; set; }
        [Required]
        public string TipoRol { get; set; } = null!;
        [JsonIgnore]
        public List<Usuario>? Usuarios { get; set; }

    }
}
