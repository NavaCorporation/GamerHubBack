using GamerHub_Backend.Entities;

namespace GamerHub_Backend.Repository
{
    public class ComentarioRepository : IComentariosRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public ComentarioRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Comentario?> ObtenerPorId(int id)
        {
            return await _dbContext.Comentarios.FindAsync(id);
        }

        public async Task<int?> Crear(Comentario comentario)
        {
            _dbContext.Comentarios.Add(comentario);
            await _dbContext.SaveChangesAsync();
            return comentario.Id;
        }

        public async Task<bool> Editar(Comentario comentario)
        {
            _dbContext.Comentarios.Update(comentario);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> Eliminar(int id)
        {
            var comentario = await ObtenerPorId(id);
            if (comentario == null)
            {
                return false;
            }

            _dbContext.Comentarios.Remove(comentario);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}
