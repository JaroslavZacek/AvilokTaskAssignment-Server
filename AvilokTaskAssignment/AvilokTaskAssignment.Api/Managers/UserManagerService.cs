using AvilokTaskAssignment.Data.Models;
using Microsoft.AspNetCore.Identity;

namespace AvilokTaskAssignment.Api.Managers
{
    public class UserManagerService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserManagerService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task AssignRoleAsync(Guid userId, string roleName)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                throw new Exception("Uživatel nebyl nalezen.");


            if (await _userManager.IsInRoleAsync(user, roleName))
                throw new Exception("Uživatel již má tuto roli.");


            var result = await _userManager.AddToRoleAsync(user, roleName);

            if (!result.Succeeded)
                throw new Exception(string.Join(", ", result.Errors.Select(e => e.Description)));
            
        }
    }
}
