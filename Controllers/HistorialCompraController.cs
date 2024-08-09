using Microsoft.AspNetCore.Mvc;
using GamerHub_Backend.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using GamerHub_Backend.Repository;
using static GamerHub_Backend.Repository.IRepositorioHistorial;

namespace GamerHub_Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistorialCompraController : ControllerBase
    {
        
        private readonly IRepositorioHistorial _repositorioHistorial;

        public HistorialCompraController(IRepositorioHistorial repositorioHistorial) {
            _repositorioHistorial = repositorioHistorial; 
        }


        [HttpGet("fecha/{fecha}")]
        public async Task<IActionResult> GetByFecha(DateTime fecha)
        {
            var historialVenta = await _repositorioHistorial.ObtenerPorFecha(fecha);
            if (historialVenta == null)
            {
                return NotFound();
            }
            return Ok(historialVenta);
        }

        [HttpGet("orden/{idOrdenCompra}")]
        public async Task<IActionResult> GetByOrdenCompra(int idOrdenCompra)
        {
            var historialVenta = await _repositorioHistorial.ObtenerPorOrdenCompra(idOrdenCompra);
            if (historialVenta == null)
            {
                return NotFound();
            }
            return Ok(historialVenta);
        }

        [HttpDelete("orden/{idOrdenCompra}")]
        public async Task<IActionResult> DeleteByOrdenCompra(int idOrdenCompra)
        {
            var eliminado = await _repositorioHistorial.EliminarPorOrdenCompra(idOrdenCompra);
            if (!eliminado)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}