using Microsoft.AspNetCore.Mvc;
using GardenAPI.DAL;
using GardenAPI.Models;

namespace GardenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserTemplateController : ControllerBase
    {
        private readonly UserTemplateDAL _userTemplateDAL;

        public UserTemplateController()
        {
            _userTemplateDAL = new UserTemplateDAL();
        }

        [HttpPost]
        public IActionResult CreateUserTemplate([FromBody] UserTemplate userTemplate)
        {
            _userTemplateDAL.CreateUserTemplate(userTemplate);
            return CreatedAtAction(nameof(GetUserTemplateById), new { userId = userTemplate.UserID, templateId = userTemplate.TemplateID }, userTemplate);
        }

        [HttpGet("{userId}/{templateId}")]
        public IActionResult GetUserTemplateById(int userId, int templateId)
        {
            var userTemplate = _userTemplateDAL.GetUserTemplateById(userId, templateId);
            if (userTemplate == null)
                return NotFound();
            return Ok(userTemplate);
        }

        [HttpGet]
        public IActionResult GetAllUserTemplates()
        {
            var userTemplates = _userTemplateDAL.GetAllUserTemplates();
            return Ok(userTemplates);
        }

        [HttpDelete("{userId}/{templateId}")]
        public IActionResult DeleteUserTemplate(int userId, int templateId)
        {
            _userTemplateDAL.DeleteUserTemplate(userId, templateId);
            return NoContent();
        }
    }
}
