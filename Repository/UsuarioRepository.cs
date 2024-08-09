using GamerHub_Backend.Entities;
using Microsoft.EntityFrameworkCore;

namespace GamerHub_Backend.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly ApplicationDBContext _dbContext;

        public UsuarioRepository(ApplicationDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Usuario?> GetByEmailAsync(string email)
        {
            return await _dbContext.Usuarios.SingleOrDefaultAsync(u => u.Correo == email);
        }

        public async Task AddAsync(Usuario usuario)
        {
            await _dbContext.Usuarios.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();
        }
        public async Task<Usuario?> LlamarPorNombre(string nombreUsuario)
        {
            return await _dbContext.Usuarios.SingleOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);

        }
        public async Task<int?> Crear(Usuario usuario)
        {
            _dbContext.Usuarios.Add(usuario);
            await _dbContext.SaveChangesAsync();
            return usuario.Id;
        }
        public async Task<Usuario?> ObtenerPorId(int id)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(usuario => usuario.Id == id);
        }
        public async Task<Rol?> VerificarRol(int rolId)
        {
            return await _dbContext.Roles.FindAsync(rolId);
        }
        public async Task<Usuario?> ModificarUsuario(Usuario usuario)
        {
            var usuarioExistente = await _dbContext.Usuarios.FindAsync(usuario.Id);
            if (usuarioExistente != null)
            {
                
                usuarioExistente.Nombre = usuario.Nombre;
                usuarioExistente.Apellido = usuario.Apellido;
                usuarioExistente.Correo = usuario.Correo;
                usuarioExistente.Telefono = usuario.Telefono;
              
                await _dbContext.SaveChangesAsync();
                return usuarioExistente;
            }
            return null;
        }



        public async Task<Usuario?> ModificarFoto(int id, byte[] nuevaFoto)
        {
            var usuarioExistente = await _dbContext.Usuarios .FindAsync(id);
            if (usuarioExistente != null)
            {
                usuarioExistente.FotoPerfil = nuevaFoto;
                _dbContext.Entry(usuarioExistente).Property(u => u.FotoPerfil).IsModified = true;
                await _dbContext.SaveChangesAsync();
                return usuarioExistente;
            }
            return null;
        }

    }
}

