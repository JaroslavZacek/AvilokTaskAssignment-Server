using AvilokTaskAssignment.Api.DTO;

namespace AvilokTaskAssignment.Api.Interfaces
{
    public interface IUserManagerService
    {
        Task<IEnumerable<UserListDto>> GetUsersAsync();
        Task AssignRoleAsync(Guid userId, string roleName);
    }
}
