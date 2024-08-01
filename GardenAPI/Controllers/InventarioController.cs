using Microsoft.AspNetCore.Mvc;
using GardenAPI.DAL;
using GardenAPI.Models;

namespace GardenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventarioController : ControllerBase
    {
        private readonly InventarioDAL _inventarioDAL;

        public InventarioController()
        {
            _inventarioDAL = new InventarioDAL();
        }

        [HttpPost]
        public IActionResult CreateInventario([FromBody] Inventario inventario)
        {
            _inventarioDAL.CreateInventario(inventario);
            return CreatedAtAction(nameof(GetInventarioById), new { id = inventario.ID }, inventario);
        }

        [HttpGet("{id}")]
        public IActionResult GetInventarioById(int id)
        {
            var inventario = _inventarioDAL.GetInventarioById(id);
            if (inventario == null)
                return NotFound();
            return Ok(inventario);
        }

        [HttpGet]
        public IActionResult GetAllInventarios()
        {
            var inventarios = _inventarioDAL.GetAllInventarios();
            return Ok(inventarios);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateInventario(int id, [FromBody] Inventario inventario)
        {
            if (id != inventario.ID)
                return BadRequest();
            _inventarioDAL.UpdateInventario(inventario);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteInventario(int id)
        {
            _inventarioDAL.DeleteInventario(id);
            return NoContent();
        }
    }
}
