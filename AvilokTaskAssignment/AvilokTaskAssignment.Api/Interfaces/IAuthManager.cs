using AvilokTaskAssignment.Api.DTO;

namespace AvilokTaskAssignment.Api.Interfaces
{
    public interface IAuthManager
    {
        Task RegisterAsync(RegisterUserDto registerUserDto);
        Task LoginAsync(LoginDto loginDto);
        Task LogoutAsync();
    }
}
