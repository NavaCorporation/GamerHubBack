using GamerHub_Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamerHub_Backend.Repository
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public CategoriaRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Categoria>> ObtenerTodasLasCategoriasAsync()
        {
            return  _dbContext.Categorias.ToList();
        }

        public async Task<Categoria> ObtenerCategoriaPorIdAsync(int id)
        {
            return await _dbContext.Categorias.Include(c => c.Productos).FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Categoria> AgregarCategoriaAsync(Categoria categoria)
        {
            _dbContext.Categorias.Add(categoria);
            await _dbContext.SaveChangesAsync();
            return categoria;
        }

        public async Task ActualizarCategoriaAsync(Categoria categoria)
        {
            _dbContext.Entry(categoria).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task EliminarCategoriaAsync(int id)
        {
            var categoria = await _dbContext.Categorias.FindAsync(id);
            _dbContext.Categorias.Remove(categoria);
            await _dbContext.SaveChangesAsync();
        }

    }
}
