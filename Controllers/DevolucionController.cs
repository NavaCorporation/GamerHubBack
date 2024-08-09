
using GamerHub_Backend.Entities;
using GamerHub_Backend.Repository;
using Microsoft.AspNetCore.Mvc;
namespace GamerHub_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
  
        public class DevolucionController : ControllerBase
        {
            private readonly IDevolucionRepository _devolucionRepository;
            

        public DevolucionController(IDevolucionRepository devolucionRepository)
            {
                _devolucionRepository = devolucionRepository;
            }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var devolucion = await _devolucionRepository.ObtenerPorId(id);
            if (devolucion == null)
            {
                return NotFound();
            }

            var resultado = new
            {
                OrdenCompra = devolucion.OrdenCompra,
                Razon = devolucion.Razon,
                Estado = devolucion.Estado
            };

            return Ok(resultado);
        }
        [HttpGet]
        public async Task<IActionResult> ObtenerTodas()
        {
            var devoluciones = await _devolucionRepository.ObtenerTodas();
            return Ok(devoluciones);
        }
        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] Devolucion devolucion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            
            var nuevaDevolucion = new Devolucion
            {
                OrdenCompra = devolucion.OrdenCompra,
                Razon = devolucion.Razon,
                Estado = devolucion.Estado,
               
                IdOreden = devolucion.IdOreden,
                IdProducto = devolucion.IdProducto,
                FechaDevolucion = DateTime.UtcNow 
            };

            var devolucionId = await _devolucionRepository.Crear(nuevaDevolucion);

            if (devolucionId == null)
            {
                return StatusCode(500, "Ocurrió un error al crear la devolución.");
            }

            return CreatedAtAction(nameof(Get), new { id = devolucionId }, nuevaDevolucion);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Devolucion devolucion)
        {
            if (id != devolucion.Id)
            {
                return BadRequest();
            }

            var result = await _devolucionRepository.Actualizar(devolucion);
            if (!result)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al actualizar la devolución.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _devolucionRepository.Eliminar(id);
            if (!result)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}