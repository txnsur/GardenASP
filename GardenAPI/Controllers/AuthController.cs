using Microsoft.AspNetCore.Mvc;
using GardenAPI.Models;
using GardenAPI.DAL;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;

namespace GardenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UsuarioDAL _usuarioDAL;
        private readonly ILogger<AuthController> _logger;

        public AuthController(UsuarioDAL usuarioDAL, ILogger<AuthController> logger)
        {
            _usuarioDAL = usuarioDAL;
            _logger = logger;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (_usuarioDAL.GetUsuarioByEmail(model.Email) != null)
            {
                return BadRequest("User with this email already exists.");
            }

            var usuario = new Usuario
            {
                Email = model.Email,
                Password = HashPassword(model.Password),
                Role = "client",
                FirstName = model.FirstName,
                LastName = model.LastName,
                Street = model.Street,
                Zip = model.Zip,
                City = model.City,
                State = model.State,
                Country = model.Country
            };

            _usuarioDAL.CreateUsuario(usuario);

            return Ok("User registered successfully.");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _logger.LogInformation("Attempting login for user: {Email}", model.Email);

            var usuario = _usuarioDAL.GetUsuarioByEmail(model.Email);
            if (usuario == null)
            {
                _logger.LogWarning("User not found: {Email}", model.Email);
                return Unauthorized("Invalid email or password.");
            }

            if (!VerifyPassword(model.Password, usuario.Password))
            {
                _logger.LogWarning("Invalid password for user: {Email}", model.Email);
                return Unauthorized("Invalid email or password.");
            }

            _logger.LogInformation("Login successful for user: {Email}", model.Email);
            return Ok("Login successful.");
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private bool VerifyPassword(string enteredPassword, string storedPasswordHash)
        {
            string enteredPasswordHash = HashPassword(enteredPassword);
            return enteredPasswordHash == storedPasswordHash;
        }
    }
}
