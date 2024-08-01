using Microsoft.AspNetCore.Mvc;
using GardenAPI.DAL;
using GardenAPI.Models;

namespace GardenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlantillaController : ControllerBase
    {
        private readonly PlantillaDAL _plantillaDAL;

        public PlantillaController()
        {
            _plantillaDAL = new PlantillaDAL();
        }

        [HttpPost]
        public IActionResult CreatePlantilla([FromBody] Plantilla plantilla)
        {
            _plantillaDAL.CreatePlantilla(plantilla);
            return CreatedAtAction(nameof(GetPlantillaById), new { id = plantilla.ID }, plantilla);
        }

        [HttpGet("{id}")]
        public IActionResult GetPlantillaById(int id)
        {
            var plantilla = _plantillaDAL.GetPlantillaById(id);
            if (plantilla == null)
                return NotFound();
            return Ok(plantilla);
        }

        [HttpGet]
        public IActionResult GetAllPlantillas()
        {
            var plantillas = _plantillaDAL.GetAllPlantillas();
            return Ok(plantillas);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePlantilla(int id, [FromBody] Plantilla plantilla)
        {
            if (id != plantilla.ID)
                return BadRequest();
            _plantillaDAL.UpdatePlantilla(plantilla);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePlantilla(int id)
        {
            _plantillaDAL.DeletePlantilla(id);
            return NoContent();
        }
    }
}
