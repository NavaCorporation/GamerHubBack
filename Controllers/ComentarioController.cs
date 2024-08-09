using GamerHub_Backend.Entities;
using GamerHub_Backend.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GamerHub_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComentarioController : ControllerBase
    {
        private readonly IComentariosRepository _comentarioRepository;

        public ComentarioController(IComentariosRepository comentarioRepository)
        {
            _comentarioRepository = comentarioRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Comentario comentario)
        {
            try
            {
                var id = await _comentarioRepository.Crear(comentario);
                return CreatedAtAction(nameof(ObtenerPorId), new { id = id }, comentario);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                return StatusCode(500, new { message = "Error al crear el comentario.", details = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comentario>> ObtenerPorId(int id)
        {
            var comentario = await _comentarioRepository.ObtenerPorId(id);
            if (comentario == null)
            {
                return NotFound();
            }
            return comentario;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Actualizar(int id, [FromBody] Comentario comentario)
        {
            if (id != comentario.Id)
            {
                return BadRequest();
            }

            var updated = await _comentarioRepository.Editar(comentario);
            if (!updated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var deleted = await _comentarioRepository.Eliminar(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
