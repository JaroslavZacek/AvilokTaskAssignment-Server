using System;
using System.Collections.Generic;
using System.Text;

using AvilokTaskAssignment.Data.Models;

namespace AvilokTaskAssignment.Data.Interfaces
{
    internal interface ITaskRepository : IBaseRepository<TaskItem>
    {
        Task<IEnumerable<TaskItem>> GetFilteredAsync(
            WorkType? workType,
            Guid? createdById,
            Guid? assignedUserId,
            Models.TaskStatus? status);
    }
}
