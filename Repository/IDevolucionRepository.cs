using GamerHub_Backend.Entities;

namespace GamerHub_Backend.Repository
{
    public interface IDevolucionRepository
    {
        Task<Devolucion?> ObtenerPorId(int id);
        Task<int?> Crear(Devolucion devolucion);
        Task<bool> Actualizar(Devolucion devolucion);
        Task<bool> Eliminar(int id);
        Task<List<Devolucion>> ObtenerTodas();
    }
}
