namespace AvilokTaskAssignment.Api.Interfaces
{
    public interface IUserManagerService
    {
        Task AssignRoleAsync(Guid userId, string roleName);
    }
}
