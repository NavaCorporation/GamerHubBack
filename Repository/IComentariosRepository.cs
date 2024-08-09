using GamerHub_Backend.Entities;

namespace GamerHub_Backend.Repository
{
    public interface IComentariosRepository
    {
        Task<Comentario?> ObtenerPorId(int id);
        Task<int?> Crear(Comentario comentario);
        Task<bool> Editar(Comentario comentario);
        Task<bool> Eliminar(int id);
    }
}
