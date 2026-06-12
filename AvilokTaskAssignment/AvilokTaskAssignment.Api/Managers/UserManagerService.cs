using AvilokTaskAssignment.Data.Models;
using AvilokTaskAssignment.Api.Interfaces;
using AvilokTaskAssignment.Api.DTO;

using Microsoft.AspNetCore.Identity;

using Microsoft.EntityFrameworkCore;


namespace AvilokTaskAssignment.Api.Managers
{
    public class UserManagerService : IUserManagerService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserManagerService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        #region Get

        public async Task<UserDetailDto> GetDetailUserAsync(Guid userId)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());

            if (user == null)
                throw new Exception("Uživatel nebyl nalezen.");

            var roles = await _userManager.GetRolesAsync(user);

            return new UserDetailDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email ?? string.Empty,
                Roles = roles.ToList()
            };
        }

        /// <summary>
        /// Získá seznam všech uživatelů v systému, seřazených podle jména. Každý uživatel je reprezentován jako UserListDto, který obsahuje jeho ID, celé jméno a email.
        /// </summary>
        public async Task<IEnumerable<UserListDto>> GetUsersAsync()
        {
            var users = await _userManager.Users
                .OrderBy(u => u.FullName)
                .ToListAsync();

            var result = new List<UserListDto>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                result.Add(new UserListDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email ?? string.Empty,
                    Roles = roles.ToList()
                });
            }

            return result;
        }
        #endregion


        /// <summary>
        /// Nastaví roli pro uživatele. Pokud uživatel již má tuto roli, vyhodí výjimku.
        /// </summary>

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
