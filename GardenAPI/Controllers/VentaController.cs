using Microsoft.AspNetCore.Mvc;
using GardenAPI.DAL;
using GardenAPI.Models;

namespace GardenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentaController : ControllerBase
    {
        private readonly VentaDAL _ventaDAL;

        public VentaController()
        {
            _ventaDAL = new VentaDAL();
        }

        [HttpPost]
        public IActionResult CreateVenta([FromBody] Venta venta)
        {
            _ventaDAL.CreateVenta(venta);
            return CreatedAtAction(nameof(GetVentaById), new { id = venta.ID }, venta);
        }

        [HttpGet("{id}")]
        public IActionResult GetVentaById(int id)
        {
            var venta = _ventaDAL.GetVentaById(id);
            if (venta == null)
                return NotFound();
            return Ok(venta);
        }

        [HttpGet]
        public IActionResult GetAllVentas()
        {
            var ventas = _ventaDAL.GetAllVentas();
            return Ok(ventas);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateVenta(int id, [FromBody] Venta venta)
        {
            if (id != venta.ID)
                return BadRequest();
            _ventaDAL.UpdateVenta(venta);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteVenta(int id)
        {
            _ventaDAL.DeleteVenta(id);
            return NoContent();
        }
    }
}
