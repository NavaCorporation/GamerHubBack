using GamerHub_Backend.Entities;

namespace GamerHub_Backend.Repository
{
    public interface IProductoRepository
    {
        Task<IEnumerable<Producto>> ObtenerTodos();
        Task<Producto?> ObtenerPorId(int id);
        Task<int?> AgregarProductoAsync(Producto producto);
        Task ActualizarProductoAsync(Producto producto);
        Task<List<Producto>> ObtenerPorCategoriaId(int categoriaId);
        Task<Categoria?> VerificarCat(int categoriaId);
        Task EliminarProductoAsync(int id);
    }
}
