using GamerHub_Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamerHub_Backend.Repository
{
    public class RolRepository
    {
        private readonly ApplicationDBContext _context;
        public RolRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Rol>> GetAll()
        {
            return await _context.Roles.ToListAsync();
        }
        public async Task<Rol> GetById(int id)
        {
            return await _context.Roles.FindAsync(id);
        }
        public async Task Add(Rol entity)
        {
            await _context.Roles.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task Update(Rol entity)
        {
            _context.Roles.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}
