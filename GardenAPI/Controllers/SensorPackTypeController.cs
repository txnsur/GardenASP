using Microsoft.AspNetCore.Mvc;
using GardenAPI.DAL;
using GardenAPI.Models;

namespace GardenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorPackTypeController : ControllerBase
    {
        private readonly SensorPackTypeDAL _sensorPackTypeDAL;

        public SensorPackTypeController()
        {
            _sensorPackTypeDAL = new SensorPackTypeDAL();
        }

        [HttpPost]
        public IActionResult CreateSensorPackType([FromBody] SensorPackType sensorPackType)
        {
            _sensorPackTypeDAL.CreateSensorPackType(sensorPackType);
            return CreatedAtAction(nameof(GetSensorPackTypeById), new { id = sensorPackType.ID }, sensorPackType);
        }

        [HttpGet("{id}")]
        public IActionResult GetSensorPackTypeById(int id)
        {
            var sensorPackType = _sensorPackTypeDAL.GetSensorPackTypeById(id);
            if (sensorPackType == null)
                return NotFound();
            return Ok(sensorPackType);
        }

        [HttpGet]
        public IActionResult GetAllSensorPackTypes()
        {
            var sensorPackTypes = _sensorPackTypeDAL.GetAllSensorPackTypes();
            return Ok(sensorPackTypes);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSensorPackType(int id, [FromBody] SensorPackType sensorPackType)
        {
            if (id != sensorPackType.ID)
                return BadRequest();
            _sensorPackTypeDAL.UpdateSensorPackType(sensorPackType);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSensorPackType(int id)
        {
            _sensorPackTypeDAL.DeleteSensorPackType(id);
            return NoContent();
        }
    }
}
