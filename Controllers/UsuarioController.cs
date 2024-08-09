using GamerHub_Backend.Entities;
using GamerHub_Backend.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GamerHub_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IConfiguration _configuration;


        public UsuarioController(IUsuarioRepository usuarioRepository, IConfiguration configuration)
        {
            _usuarioRepository = usuarioRepository;
            _configuration = configuration;
        }
        
        
        [HttpPost("register")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Register([FromForm] Usuario usuario, [FromForm] IFormFile? profilePicture)
        {
            try
            {
                var existingUsuario = await _usuarioRepository.GetByEmailAsync(usuario.Correo);
                if (existingUsuario != null)
                {
                    return BadRequest(new { message = "El correo ya está registrado." });
                }

                var rol = await _usuarioRepository.VerificarRol(usuario.RolId);
                if (rol == null)
                {
                    return BadRequest(new { message = "Rol no válido." });
                }

                if (profilePicture != null && profilePicture.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await profilePicture.CopyToAsync(memoryStream);
                        usuario.FotoPerfil = memoryStream.ToArray();
                    }
                }

                usuario.Rol = null;
                await _usuarioRepository.AddAsync(usuario);

                return Ok(new { message = "Usuario registrado exitosamente." });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                return StatusCode(500, new { message = "Error al registrar el usuario.", details = ex.InnerException?.Message ?? ex.Message });
            }
        }
        
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> ObtenerUsuarioPorId(int id)
        {
            var usuario = await _usuarioRepository.ObtenerPorId(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return usuario;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel login)
        {
            var user = await _usuarioRepository.GetByEmailAsync(login.Correo);
            if (user == null || user.Contrasena != login.Contrasena)
            {
                return Unauthorized(new { message = "Correo o contraseña inválidos" });
            }
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ModificarUsuario(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return BadRequest("ID del usuario no coincide.");
            }

            var usuarioExistente = await _usuarioRepository.ObtenerPorId(id);
            if (usuarioExistente == null)
            {
                return NotFound();
            }

            // Puedes actualizar solo los campos permitidos
            usuario.Id = id; // Asegúrate de que el ID coincida
            var resultado = await _usuarioRepository.ModificarUsuario(usuario);

            if (resultado == null)
            {
                return NotFound();
            }

            return Ok(resultado);
        }

        [HttpPut("update-photo/{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ModificarFoto(int id, [FromForm] IFormFile profilePicture)
        {
            try
            {
                if (profilePicture == null || profilePicture.Length == 0)
                {
                    return BadRequest(new { message = "Archivo de foto no proporcionado." });
                }

                using (var memoryStream = new MemoryStream())
                {
                    await profilePicture.CopyToAsync(memoryStream);
                    var nuevaFoto = memoryStream.ToArray();
                    var usuarioActualizado = await _usuarioRepository.ModificarFoto(id, nuevaFoto);
                    if (usuarioActualizado == null)
                    {
                        return NotFound(new { message = "Usuario no encontrado." });
                    }
                    return Ok(new { message = "Foto de perfil actualizada exitosamente." });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                return StatusCode(500, new { message = "Error al actualizar la foto de perfil.", details = ex.InnerException?.Message ?? ex.Message });
            }

        }
        
        [HttpGet("{id}/fotoPerfil")]
        public async Task<IActionResult> GetFotoPerfil(int id)
        {
            var usuario = await _usuarioRepository.ObtenerPorId(id);
            if (usuario == null || usuario.FotoPerfil == null)
            {
                return NotFound();
            }

            string base64Image = Convert.ToBase64String(usuario.FotoPerfil);
            return Content(base64Image, "text/plain");
        }
    }

        public class LoginModel
        {
            public string Correo { get; set; } = null!;
            public string Contrasena { get; set; } = null!;
        }
    



}

