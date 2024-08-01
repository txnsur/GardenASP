using Microsoft.AspNetCore.Mvc;
using GardenAPI.DAL;
using GardenAPI.Models;
using Microsoft.Extensions.Logging;

namespace GardenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly UsuarioDAL _usuarioDAL;
        private readonly ILogger<UsuarioController> _logger;

        public UsuarioController(UsuarioDAL usuarioDAL, ILogger<UsuarioController> logger)
        {
            _usuarioDAL = usuarioDAL;
            _logger = logger;
        }

        [HttpPost]
        public IActionResult CreateUsuario([FromBody] Usuario usuario)
        {
            _usuarioDAL.CreateUsuario(usuario);
            return CreatedAtAction(nameof(GetUsuarioById), new { id = usuario.ID }, usuario);
        }

        [HttpGet("{id}")]
        public IActionResult GetUsuarioById(int id)
        {
            var usuario = _usuarioDAL.GetUsuarioById(id);
            if (usuario == null)
                return NotFound();
            return Ok(usuario);
        }

        [HttpGet]
        public IActionResult GetAllUsuarios()
        {
            var usuarios = _usuarioDAL.GetAllUsuarios();
            return Ok(usuarios);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUsuario(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.ID)
                return BadRequest();
            _usuarioDAL.UpdateUsuario(usuario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUsuario(int id)
        {
            _usuarioDAL.DeleteUsuario(id);
            return NoContent();
        }
    }
}
