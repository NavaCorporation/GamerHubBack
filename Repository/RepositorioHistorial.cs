using GamerHub_Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamerHub_Backend.Repository
{
    public class RepositorioHistorial : IRepositorioHistorial
    {
        private readonly ApplicationDBContext _dbContext;

        public RepositorioHistorial(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<HistorialVenta>> ObtenerTodos()
        {
            return await _dbContext.HistorialVentas
                .Include(hv => hv.OrdenCompra)
                .ThenInclude(o => o.DetallesCompras)
                .ThenInclude(dc => dc.Producto)
                .Include(hv => hv.Usuario)
                .ToListAsync();
        }

        public async Task<HistorialVenta?> ObtenerPorFecha(DateTime fecha)
        {
            return await _dbContext.HistorialVentas
                .Include(hv => hv.OrdenCompra)
                .ThenInclude(o => o.DetallesCompras)
                .ThenInclude(dc => dc.Producto)
                .Include(hv => hv.Usuario)
                .FirstOrDefaultAsync(hv => hv.FechaVenta.HasValue && hv.FechaVenta.Value.Date == fecha.Date);
        }

        public async Task<HistorialVenta?> ObtenerPorOrdenCompra(int idOrdenCompra)
        {
            return await _dbContext.HistorialVentas
                .Include(hv => hv.OrdenCompra)
                .ThenInclude(o => o.DetallesCompras)
                .ThenInclude(dc => dc.Producto)
                .Include(hv => hv.Usuario)
                .FirstOrDefaultAsync(hv => hv.IdOrden == idOrdenCompra);
        }

        public async Task<bool> EliminarPorOrdenCompra(int idOrdenCompra)
        {
            var historialVenta = await ObtenerPorOrdenCompra(idOrdenCompra);
            if (historialVenta == null)
            {
                return false;
            }

            _dbContext.HistorialVentas.Remove(historialVenta);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}
