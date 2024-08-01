using Microsoft.AspNetCore.Mvc;
using GardenAPI.DAL;
using GardenAPI.Models;

namespace GardenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorController : ControllerBase
    {
        private readonly SensorDAL _sensorDAL;

        public SensorController()
        {
            _sensorDAL = new SensorDAL();
        }

        [HttpPost]
        public IActionResult CreateSensor([FromBody] Sensor sensor)
        {
            _sensorDAL.CreateSensor(sensor);
            return CreatedAtAction(nameof(GetSensorById), new { id = sensor.ID }, sensor);
        }

        [HttpGet("{id}")]
        public IActionResult GetSensorById(int id)
        {
            var sensor = _sensorDAL.GetSensorById(id);
            if (sensor == null)
                return NotFound();
            return Ok(sensor);
        }

        [HttpGet]
        public IActionResult GetAllSensors()
        {
            var sensors = _sensorDAL.GetAllSensors();
            return Ok(sensors);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSensor(int id, [FromBody] Sensor sensor)
        {
            if (id != sensor.ID)
                return BadRequest();
            _sensorDAL.UpdateSensor(sensor);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSensor(int id)
        {
            _sensorDAL.DeleteSensor(id);
            return NoContent();
        }
    }
}
