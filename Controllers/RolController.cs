using GamerHub_Backend.Entities;
using GamerHub_Backend.Repository;
using Microsoft.AspNetCore.Mvc;

namespace GamerHub_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly RolRepository _rolRepository;
        public RolController(RolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<Rol>> GetRoles()
        {
            return await _rolRepository.GetAll();
        }
        [HttpGet("{id}")]
        public async Task<Rol> GetRol(int id)
        {

            return await _rolRepository.GetById(id);
        }
        [HttpPost]
        public async Task<IActionResult> PostRol([FromBody] Rol rol)
        {
            await _rolRepository.Add(rol);
            return CreatedAtAction(nameof(GetRol), new { id = rol.Id }, rol);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutRol(int id, [FromBody] Rol rol)
        {
            if (id != rol.Id)
            {
                return BadRequest();
            }
            await _rolRepository.Update(rol);
            return NoContent();
        }

    }
}
