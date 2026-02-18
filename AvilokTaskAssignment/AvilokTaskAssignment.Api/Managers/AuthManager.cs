using Microsoft.AspNetCore.Identity;
using AvilokTaskAssignment.Data.Models;
using AvilokTaskAssignment.Api.DTO;

namespace AvilokTaskAssignment.Api.Managers
{
    public class AuthManager
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
        /// <param name="registerUserDto"></param>
        /// <returns></returns>
        /// <exception cref="Exception">Exception při nepovedené registraci. Vypíše všechny za s sebou.</exception>
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
        /// <param name="loginDto"></param>
        /// <returns>Přihlášení uživatele</returns>
        /// <exception cref="UnauthorizedAccessException">Exception při zadání neplatných přihlašovacích údajů.</exception>
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
        /// <returns>Odhlášení uživatele</returns>
        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
