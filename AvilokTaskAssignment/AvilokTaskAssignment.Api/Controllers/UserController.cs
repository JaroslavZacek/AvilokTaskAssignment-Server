using AvilokTaskAssignment.Api.DTO;
using AvilokTaskAssignment.Api.Managers;
using AvilokTaskAssignment.Api.Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvilokTaskAssignment.Api.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserManagerService _userManagerService;

        public UserController(IUserManagerService userManagerService)
        {
            _userManagerService = userManagerService;
        }

        #region Get

        /// <summary>
        /// Crud operace pro získání detailů uživatele podle jeho ID.
        /// </summary>
        [HttpGet("{userId}")]
        [Authorize(Roles = "Admin, Leader Developer, Leader Graphic, Leader Story")]
        public async Task<ActionResult<UserDetailDto>> GetDetailUser(Guid userId)
        {
            var user = await _userManagerService.GetDetailUserAsync(userId);

            return Ok(user);
        }

        /// <summary>
        /// Crud operace pro získání všech uživatelů.
        /// </summary>
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userManagerService.GetUsersAsync();
            return Ok(users);
        }

        #endregion

        #region Post

        /// <summary>
        /// Crud operace pro přiřazení role uživateli.
        /// </summary>
        [HttpPost("{userId}/assign-role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRole(Guid userId, [FromBody] AssignRoleDto dto)
        {
            await _userManagerService.AssignRoleAsync(userId, dto.RoleName);

            return Ok(new
            {
                Message = "Role byla přidělena úspěsně."
            });
        }

        #endregion

        #region Put
        #endregion

        #region Delete
        /// <summary>
        /// Crud operace pro odebrání role uživateli.
        /// </summary>
        [HttpDelete("{userId}/remove-role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RemoveRole(Guid userId, [FromBody] RemoveRoleDto dto)
        {
            await _userManagerService.RemoveRoleAsync(userId, dto.RoleName);

            return Ok(new
            {
                Message = "Role byla odebrána úspěšně."
            });
        }
        #endregion


    }
}
