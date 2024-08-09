using GamerHub_Backend.Entities;
using GamerHub_Backend.Repository;
using GamerHub_Backend.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace GamerHub_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : Controller
    {
        private readonly IProductoRepository _productoRepository;
        public ProductosController(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductos()
        {
            var productos = await _productoRepository.ObtenerTodos();
            return Ok(productos);
        }
        [HttpGet("categoria/{categoriaId}")]
        public async Task<IActionResult> GetProductosPorCategoria(int categoriaId)
        {
            var productos = await _productoRepository.ObtenerPorCategoriaId(categoriaId);
            if (productos == null || !productos.Any())
            {
                return NotFound(new { message = "No se encontraron productos para esta categoría." });
            }
            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Producto>> ObtenerProducto(int id)
        {
            var producto = await _productoRepository.ObtenerPorId(id);

            if (producto == null)
            {
                return NotFound();
            }

            return Ok(producto);
        }
        [HttpGet("{id}/imagen")]
        public async Task<IActionResult> GetImagen(int id)
        {
            var producto = await _productoRepository.ObtenerPorId(id);
            if (producto == null || producto.Imagen == null)
            {
                return NotFound();
            }

            string base64Image = Convert.ToBase64String(producto.Imagen);
            return Content(base64Image, "text/plain");
        }

        [HttpPost("Registrar")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AgregarProducto([FromForm] Producto producto, [FromForm] IFormFile? imagen)
        {
            Console.WriteLine($"Producto: {producto.NombreProducto}, Precio: {producto.Precio}, Imagen: {imagen?.FileName}");

            try
            {
                var categoria = await _productoRepository.VerificarCat(producto.CategoriaId);
                if (categoria == null)
                {
                    return BadRequest(new { message = "Categoria no valida" });
                }
                if (imagen != null && imagen.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await imagen.CopyToAsync(memoryStream);
                        producto.Imagen = memoryStream.ToArray();
                    }
                }

                producto.Categoria = null;
                await _productoRepository.AgregarProductoAsync(producto);
                return Ok(new { message = "Producto registrado" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                return StatusCode(500, new { message = "Error al agregar el producto.", details = ex.InnerException?.Message ?? ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> ActualizarProducto(int id, Producto producto)
        {
            if (id != producto.Id)
            {
                return BadRequest();
            }

            if (producto.CategoriaId <= 0)
            {
                return BadRequest("El Id de la categoría debe ser válido.");
            }



            await _productoRepository.ActualizarProductoAsync(producto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> EliminarProducto(int id)
        {
            await _productoRepository.EliminarProductoAsync(id);
            return NoContent();
        }

        [HttpPut("actualizar/{id}")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> ActualizarProducto(int id, [FromForm] Producto producto, [FromForm] IFormFile? imagen)
        {
            try
            {
                var productoEncontrado = await _productoRepository.ObtenerPorId(id);
                if (productoEncontrado == null)
                {
                    return NotFound(new { message = "Producto no encontrado" });
                }

                if (producto.CategoriaId <= 0)
                {
                    return BadRequest("El Id de la categoría debe ser válido.");
                }

                var categoria = await _productoRepository.VerificarCat(producto.CategoriaId);
                if (categoria == null)
                {
                    return BadRequest(new { message = "Categoria no valida" });
                }

                if (imagen != null && imagen.Length > 0)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await imagen.CopyToAsync(memoryStream);
                        productoEncontrado.Imagen = memoryStream.ToArray();  // Actualizar imagen
                    }
                }

                // Actualizar otros campos del producto
                productoEncontrado.NombreProducto = producto.NombreProducto;
                productoEncontrado.Precio = producto.Precio;
                productoEncontrado.Caracteristicas = producto.Caracteristicas;
                productoEncontrado.Descripcion = producto.Descripcion;
                productoEncontrado.Stock = producto.Stock;
                productoEncontrado.CategoriaId = producto.CategoriaId;

                await _productoRepository.ActualizarProductoAsync(productoEncontrado);
                return Ok(new { message = "Producto actualizado" });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.InnerException?.Message ?? ex.Message);
                return StatusCode(500, new { message = "Error al actualizar el producto.", details = ex.InnerException?.Message ?? ex.Message });
            }
        }



    }
}

