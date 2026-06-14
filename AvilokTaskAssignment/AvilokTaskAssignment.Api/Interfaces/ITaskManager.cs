using AvilokTaskAssignment.Api.DTO;
using AvilokTaskAssignment.Data.Models;

using TaskStatus = AvilokTaskAssignment.Data.Models.TaskStatus;

namespace AvilokTaskAssignment.Api.Interfaces
{
    public interface ITaskManager
    {
        Task<IEnumerable<TaskListDto>> GetFilteredTasksAsync(
        WorkType? workType,
        Guid? createdById,
        Guid? assignedUserId,
        TaskStatus? status);

        Task<Guid> CreateTaskAsync(CreateTaskDto dto, Guid createdById);

        Task AssignTaskAsync(Guid taskId, Guid? userId, List<string> roles);

        Task UpdateStatusAsync(Guid taskId, TaskStatus newStatus);

        Task<bool> DeleteTaskAsync(Guid taskId);
        Task<TaskDetailDto> GetTaskDetailAsync(Guid taskId);

        Task UpdateTaskAsync(Guid taskId, UpdateTaskDto dto, List<string> roles);
    }
}
