using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using AvilokTaskAssignment.Api.DTO;
using AvilokTaskAssignment.Api.Interfaces;

using System.Security.Claims;


namespace AvilokTaskAssignment.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager _authManager;

        public AuthController(IAuthManager authManager)
        {
            _authManager = authManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            await _authManager.RegisterAsync(registerUserDto);
            return Ok("Uživatel vytvořen.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            await _authManager.LoginAsync(loginDto);
            return Ok("Přihlášení úspěšné.");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authManager.LogoutAsync();
            return Ok("Odhlášení úspěšné.");
        }

        [HttpGet("me")]
        [Authorize]
        public IActionResult Me()
        {
            return Ok(new CurrentUserDto
            {
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty,
                Email = User.Identity?.Name,
                Roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList()
            });
        }
    }
}
