using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace GamerHub_Backend.Entities
{
    public class OrdenCompra
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; } = null!;
        [Column(TypeName = "Date")]
        public DateTime FechaOrden { get; set; }
        [Required]
        public decimal MontoTotal { get; set; }
        public string Estado { get; set; } = null!;
        public string? TipoPaga { get; set; }
        [JsonIgnore]
        public List<VerificacionEnvio>? VerificacionEnvios { get; set; }
        [JsonIgnore]
        public List<DetallesCompra>? DetallesCompras { get; set; }
        [JsonIgnore]
        public List<HistorialVenta>? HistorialVenta { get; set; }
        [JsonIgnore]
        public List<Devolucion>? Devoluciones { get; set; }

    }
}
