using Microsoft.AspNetCore.Mvc;
using GardenAPI.DAL;
using GardenAPI.Models;

namespace GardenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserMembershipController : ControllerBase
    {
        private readonly UserMembershipDAL _userMembershipDAL;

        public UserMembershipController()
        {
            _userMembershipDAL = new UserMembershipDAL();
        }

        [HttpPost]
        public IActionResult CreateUserMembership([FromBody] UserMembership userMembership)
        {
            _userMembershipDAL.CreateUserMembership(userMembership);
            return CreatedAtAction(nameof(GetUserMembershipById), new { id = userMembership.ID }, userMembership);
        }

        [HttpGet("{id}")]
        public IActionResult GetUserMembershipById(int id)
        {
            var userMembership = _userMembershipDAL.GetUserMembershipById(id);
            if (userMembership == null)
                return NotFound();
            return Ok(userMembership);
        }

        [HttpGet]
        public IActionResult GetAllUserMemberships()
        {
            var userMemberships = _userMembershipDAL.GetAllUserMemberships();
            return Ok(userMemberships);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUserMembership(int id, [FromBody] UserMembership userMembership)
        {
            if (id != userMembership.ID)
                return BadRequest();
            _userMembershipDAL.UpdateUserMembership(userMembership);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUserMembership(int id)
        {
            _userMembershipDAL.DeleteUserMembership(id);
            return NoContent();
        }
    }
}
