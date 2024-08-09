using GamerHub_Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamerHub_Backend.Repository
{
    public class DevolucionRepository : IDevolucionRepository
    {
        private readonly ApplicationDBContext _dbContext;
        public DevolucionRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Devolucion?> ObtenerPorId(int id)
        {
            return await _dbContext.Devolucions
                .Where(d => d.Id == id)
                .Select(d => new Devolucion
                {
                    OrdenCompra = d.OrdenCompra,
                    Razon = d.Razon,
                    Estado = d.Estado
                })
                .FirstOrDefaultAsync();
        }

        public async Task<int?> Crear(Devolucion devolucion)
        {
            var nuevaDevolucion = new Devolucion
            {
                OrdenCompra = devolucion.OrdenCompra,
                Razon = devolucion.Razon,
                Estado = devolucion.Estado,
                IdOreden = devolucion.IdOreden,
                IdProducto = devolucion.IdProducto,
                FechaDevolucion = devolucion.FechaDevolucion
            };

            _dbContext.Devolucions.Add(nuevaDevolucion);
            await _dbContext.SaveChangesAsync();
            return nuevaDevolucion.Id;
        }

        public async Task<bool> Actualizar(Devolucion devolucion)
        {
            _dbContext.Devolucions.Update(devolucion);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> Eliminar(int id)
        {
            var devolucion = await ObtenerPorId(id);
            if (devolucion == null)
            {
                return false;
            }

            _dbContext.Devolucions.Remove(devolucion);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
        public async Task<List<Devolucion>> ObtenerTodas()
        {
            return await _dbContext.Devolucions.ToListAsync();
        }
    }
}
