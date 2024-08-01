using Microsoft.AspNetCore.Mvc;
using GardenAPI.DAL;
using GardenAPI.Models;

namespace GardenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembresiaController : ControllerBase
    {
        private readonly MembresiaDAL _membresiaDAL;

        public MembresiaController()
        {
            _membresiaDAL = new MembresiaDAL();
        }

        [HttpPost]
        public IActionResult CreateMembresia([FromBody] Membresia membresia)
        {
            _membresiaDAL.CreateMembresia(membresia);
            return CreatedAtAction(nameof(GetMembresiaById), new { id = membresia.ID }, membresia);
        }

        [HttpGet("{id}")]
        public IActionResult GetMembresiaById(int id)
        {
            var membresia = _membresiaDAL.GetMembresiaById(id);
            if (membresia == null)
                return NotFound();
            return Ok(membresia);
        }

        [HttpGet]
        public IActionResult GetAllMembresias()
        {
            var membresias = _membresiaDAL.GetAllMembresias();
            return Ok(membresias);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMembresia(int id, [FromBody] Membresia membresia)
        {
            if (id != membresia.ID)
                return BadRequest();
            _membresiaDAL.UpdateMembresia(membresia);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMembresia(int id)
        {
            _membresiaDAL.DeleteMembresia(id);
            return NoContent();
        }
    }
}
