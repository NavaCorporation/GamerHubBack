using GamerHub_Backend.Entities;

namespace GamerHub_Backend.Repository
{
    public interface ICategoriaRepository
    {
        Task<List<Categoria>> ObtenerTodasLasCategoriasAsync();
        Task<Categoria> ObtenerCategoriaPorIdAsync(int id);
        Task<Categoria> AgregarCategoriaAsync(Categoria categoria);
        Task ActualizarCategoriaAsync(Categoria categoria);
        Task EliminarCategoriaAsync(int id);
    }

}
