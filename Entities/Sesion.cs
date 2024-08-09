using System.ComponentModel.DataAnnotations.Schema;

namespace GamerHub_Backend.Entities
{
    public class Sesion
    {
        public int Id { get; set; }
        public int IdUsuario { get; set; }
        public Usuario Usuario { get; set; } = null!;
        [Column(TypeName = "Date")]
        public DateTime InicioSesion { get; set; }
        [Column(TypeName = "Date")]
        public DateTime CierreSesion { get; set; }
        public string Estado { get; set; } = null!;

    }
}
