using AvilokTaskAssignment.Data.Models;
using AvilokTaskAssignment.Data.Interfaces;

using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;

namespace AvilokTaskAssignment.Data.Repositories
{
    public class TaskCommentRepository : BaseRepository<TaskComment>
    {
        public TaskCommentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<TaskComment>> GetByTaskIdAsync(Guid taskId)
        {
            return await _context.TaskComents
                .Include(c => c.Author)
                .Where(c => c.TaskId == taskId)
                .OrderBy(c => c.CreatedAt)
                .ToListAsync();
        }
    }
}
