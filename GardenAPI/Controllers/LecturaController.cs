using Microsoft.AspNetCore.Mvc;
using GardenAPI.DAL;
using GardenAPI.Models;

namespace GardenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LecturaController : ControllerBase
    {
        private readonly LecturaDAL _lecturaDAL;

        public LecturaController()
        {
            _lecturaDAL = new LecturaDAL();
        }

        [HttpPost]
        public IActionResult CreateLectura([FromBody] Lectura lectura)
        {
            _lecturaDAL.CreateLectura(lectura);
            return CreatedAtAction(nameof(GetLecturaById), new { id = lectura.ID }, lectura);
        }

        [HttpGet("{id}")]
        public IActionResult GetLecturaById(int id)
        {
            var lectura = _lecturaDAL.GetLecturaById(id);
            if (lectura == null)
                return NotFound();
            return Ok(lectura);
        }

        [HttpGet]
        public IActionResult GetAllLecturas()
        {
            var lecturas = _lecturaDAL.GetAllLecturas();
            return Ok(lecturas);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLectura(int id, [FromBody] Lectura lectura)
        {
            if (id != lectura.ID)
                return BadRequest();
            _lecturaDAL.UpdateLectura(lectura);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLectura(int id)
        {
            _lecturaDAL.DeleteLectura(id);
            return NoContent();
        }
    }
}
