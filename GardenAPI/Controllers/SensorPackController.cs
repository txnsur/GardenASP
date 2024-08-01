using Microsoft.AspNetCore.Mvc;
using GardenAPI.DAL;
using GardenAPI.Models;

namespace GardenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorPackController : ControllerBase
    {
        private readonly SensorPackDAL _sensorPackDAL;

        public SensorPackController()
        {
            _sensorPackDAL = new SensorPackDAL();
        }

        [HttpPost]
        public IActionResult CreateSensorPack([FromBody] SensorPack sensorPack)
        {
            _sensorPackDAL.CreateSensorPack(sensorPack);
            return CreatedAtAction(nameof(GetSensorPackById), new { id = sensorPack.ID }, sensorPack);
        }

        [HttpGet("{id}")]
        public IActionResult GetSensorPackById(int id)
        {
            var sensorPack = _sensorPackDAL.GetSensorPackById(id);
            if (sensorPack == null)
                return NotFound();
            return Ok(sensorPack);
        }

        [HttpGet]
        public IActionResult GetAllSensorPacks()
        {
            var sensorPacks = _sensorPackDAL.GetAllSensorPacks();
            return Ok(sensorPacks);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateSensorPack(int id, [FromBody] SensorPack sensorPack)
        {
            if (id != sensorPack.ID)
                return BadRequest();
            _sensorPackDAL.UpdateSensorPack(sensorPack);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSensorPack(int id)
        {
            _sensorPackDAL.DeleteSensorPack(id);
            return NoContent();
        }
    }
}
