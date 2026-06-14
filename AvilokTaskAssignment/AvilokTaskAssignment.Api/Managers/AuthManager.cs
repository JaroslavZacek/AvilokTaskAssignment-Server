using Microsoft.AspNetCore.Identity;
using AvilokTaskAssignment.Data.Models;
using AvilokTaskAssignment.Api.DTO;
using AvilokTaskAssignment.Api.Interfaces;

namespace AvilokTaskAssignment.Api.Managers
{
    public class AuthManager : IAuthManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthManager(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        /// Registrační metoda pro uživatele. Volá se, když nový uživatel chce vytvořit účet v aplikaci.
        /// </summary>
        public async Task RegisterAsync(RegisterUserDto registerUserDto)
        {
            var user = new ApplicationUser
            {
                Id = Guid.NewGuid(),
                Email = registerUserDto.Email,
                UserName = registerUserDto.Email,
                FullName = registerUserDto.FullName
            };

            var result = await _userManager.CreateAsync(user, registerUserDto.Password);

            if (!result.Succeeded)
                throw new Exception(string.Join(",", result.Errors.Select(e => e.Description)));
        }

        /// <summary>
        /// Přihlašovací metoda pro uživatele. Volá se, když uživatel zadá své přihlašovací údaje a chce se přihlásit do aplikace.
        /// </summary>
        public async Task LoginAsync(LoginDto loginDto)
        {
            var result = await _signInManager.PasswordSignInAsync(
                loginDto.Email,
                loginDto.Password,
                loginDto.RememberMe,
                false);

            if (!result.Succeeded)
                throw new UnauthorizedAccessException("Neplatné přihlašovací údaje.");
        }

        /// <summary>
        /// Metoda pro odhlášení uživatele. Volá se, když uživatel chce ukončit svou relaci a odhlásit se z aplikace.
        /// </summary>
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
