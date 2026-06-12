using AvilokTaskAssignment.Api.DTO;
using AvilokTaskAssignment.Api.Interfaces;
using AvilokTaskAssignment.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;


namespace AvilokTaskAssignment.Api.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthManager _authManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AuthController(IAuthManager authManager, UserManager<ApplicationUser> userManager)
        {
            _authManager = authManager;
            _userManager = userManager;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterUserDto registerUserDto)
        {
            await _authManager.RegisterAsync(registerUserDto);
            return Ok(new
            {
                Message = "Registrace úspěsná."
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            await _authManager.LoginAsync(loginDto);
            return Ok(new 
                { 
                    Message = "Přihlášení úspěšné." 
                });
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _authManager.LogoutAsync();
            return Ok(new
            {
                Message = "Odhlášení úspěšné."
            });
        }

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> Me()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
                return Unauthorized();

            return Ok(new CurrentUserDto
            {
                UserId = user.Id.ToString(),
                FullName = user.FullName,
                Roles = User.FindAll(ClaimTypes.Role).Select(r => r.Value).ToList()
            });
        }
    }
}
