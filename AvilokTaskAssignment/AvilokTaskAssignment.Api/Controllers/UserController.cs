using AvilokTaskAssignment.Api.DTO;
using AvilokTaskAssignment.Api.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AvilokTaskAssignment.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles ="Admin,Leader Developer,Leader Graphic,Leader Story")]
    public class UserController : ControllerBase
    {
        private readonly UserManagerService _userManagerService;

        public UserController(UserManagerService userManagerService)
        {
            _userManagerService = userManagerService;
        }

        #region Get
        #endregion

        #region Post

        [HttpPost("assign-role")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleDto dto)
        {
            await _userManagerService.AssignRoleAsync(dto.UserId, dto.RoleName);

            return Ok();
        }

        #endregion

        #region Put
        #endregion

        #region Delete
        #endregion


    }
}
