using Microsoft.AspNetCore.Mvc;
using GardenAPI.DAL;
using GardenAPI.Models;

namespace GardenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JardinController : ControllerBase
    {
        private readonly JardinDAL _jardinDAL;

        public JardinController()
        {
            _jardinDAL = new JardinDAL();
        }

        [HttpPost]
        public IActionResult CreateJardin([FromBody] Jardin jardin)
        {
            _jardinDAL.CreateJardin(jardin);
            return CreatedAtAction(nameof(GetJardinById), new { id = jardin.ID }, jardin);
        }

        [HttpGet("{id}")]
        public IActionResult GetJardinById(int id)
        {
            var jardin = _jardinDAL.GetJardinById(id);
            if (jardin == null)
                return NotFound();
            return Ok(jardin);
        }

        [HttpGet]
        public IActionResult GetAllJardines()
        {
            var jardines = _jardinDAL.GetAllJardines();
            return Ok(jardines);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateJardin(int id, [FromBody] Jardin jardin)
        {
            if (id != jardin.ID)
                return BadRequest();
            _jardinDAL.UpdateJardin(jardin);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteJardin(int id)
        {
            _jardinDAL.DeleteJardin(id);
            return NoContent();
        }
    }
}
