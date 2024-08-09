using GamerHub_Backend.Entities;
using GamerHub_Backend.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GamerHub_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly CategoriaRepository _categoriaRepository;

        public CategoriasController(CategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Categoria>>> ObtenerCategorias()
        {
            var categorias = await _categoriaRepository.ObtenerTodasLasCategoriasAsync();
            return categorias; 
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> ObtenerCategoria(int id)
        {
            var categoria = await _categoriaRepository.ObtenerCategoriaPorIdAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult<Categoria>> AgregarCategoria(Categoria categoria)
        {
            await _categoriaRepository.AgregarCategoriaAsync(categoria);
            return CreatedAtAction(nameof(ObtenerCategoria), new { id = categoria.Id }, categoria);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarCategoria(int id, Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return BadRequest();
            }

            await _categoriaRepository.ActualizarCategoriaAsync(categoria);
            return Ok(categoria);
        }

        [HttpPut]
        public async Task<IActionResult> ModificarCategoria([FromBody] Categoria categoria)
        {
            if (categoria == null)
            {
                return BadRequest("Categoria is null.");
            }

            await _categoriaRepository.ActualizarCategoriaAsync(categoria);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarCategoria(int id)
        {
            await _categoriaRepository.EliminarCategoriaAsync(id);
            return NoContent();
        }
    }


}
    