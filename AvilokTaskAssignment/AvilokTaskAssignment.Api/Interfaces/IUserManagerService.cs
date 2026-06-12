using AvilokTaskAssignment.Api.DTO;

namespace AvilokTaskAssignment.Api.Interfaces
{
    public interface IUserManagerService
    {
        Task<UserDetailDto> GetDetailUserAsync(Guid userId);
        Task<IEnumerable<UserListDto>> GetUsersAsync();
        Task AssignRoleAsync(Guid userId, string roleName);
        Task RemoveRoleAsync(Guid userId, string roleName);
    }
}
