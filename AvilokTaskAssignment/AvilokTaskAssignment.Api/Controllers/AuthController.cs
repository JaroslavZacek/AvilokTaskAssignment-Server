using Microsoft.AspNetCore.Mvc;
using AvilokTaskAssignment.Api.Managers;
using AvilokTaskAssignment.Api.DTO;

namespace AvilokTaskAssignment.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly AuthManager _authManager;

        public AuthController(AuthManager authManager)
        {
            _authManager = authManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            await _authManager.RegisterAsync(registerUserDto);
            return Ok("Uživatel vytvořen.");
        }
    }
}
