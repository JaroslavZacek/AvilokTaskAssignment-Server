using System;
using System.Collections.Generic;
using System.Text;

using AvilokTaskAssignment.Data.Models;

namespace AvilokTaskAssignment.Data.Interfaces
{
    public interface ITaskCommentRepository : IBaseRepository<TaskComment>
    {
        Task<IEnumerable<TaskComment>> GetByTaskIdAsync(Guid taskId);
    }
}
