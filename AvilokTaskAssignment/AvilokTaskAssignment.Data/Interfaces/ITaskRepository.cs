using System;
using System.Collections.Generic;
using System.Text;

using AvilokTaskAssignment.Data.Models;

namespace AvilokTaskAssignment.Data.Interfaces
{
    public interface ITaskRepository : IBaseRepository<TaskItem>
    {
        Task<IEnumerable<TaskItem>> GetAllAsync();
        Task<IEnumerable<TaskItem>> GetFilteredAsync(
            WorkType? workType,
            Guid? createdById,
            Guid? assignedUserId,
            Models.TaskStatus? status);
    }
}
