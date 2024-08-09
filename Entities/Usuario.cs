using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace GamerHub_Backend.Entities
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required, MaxLength(50)]
        public string Nombre { get; set; } = null!;
        [Required, MaxLength(50)]
        public string Apellido { get; set; } = null!;
        [Required, MaxLength(50)]
        public string Correo { get; set; } = null!;
        [Required, MaxLength(50)]
        public string NombreUsuario { get; set; } = null!;
        [Required, MaxLength(50)]
        public string Contrasena { get; set; } = null!;
        [Required, MaxLength(10)]
        public string Telefono { get; set; } = null!;
        [Required, MaxLength(7)]
        public string Estado { get; set; } = null!;
        public byte[]? FotoPerfil { get; set; }
        public int RolId { get; set; }
        public Rol? Rol { get; set; } = null!;
        [JsonIgnore]
        public List<HistorialVenta>? HistorialVenta { get; set; }
        [JsonIgnore]
        public List<OrdenCompra>? ordenCompras { get; set; }
        [JsonIgnore]
        public List<Sesion>? Sesiones { get; set; }
        [JsonIgnore]
        public List<Comentario>? Comentarios { get; set; }
    }
}
