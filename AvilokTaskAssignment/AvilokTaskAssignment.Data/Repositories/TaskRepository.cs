using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.EntityFrameworkCore;
using AvilokTaskAssignment.Data.Interfaces;
using AvilokTaskAssignment.Data.Models;

namespace AvilokTaskAssignment.Data.Repositories
{
    public class TaskRepository : BaseRepository<TaskItem>, ITaskRepository
    {
        public TaskRepository(ApplicationDbContext context) : base(context)
        { 
        }

        public async Task<IEnumerable<TaskItem>> GetFilteredAsync(
            WorkType? workType,
            Guid? createdById,
            Guid? assignedUserId,
            Models.TaskStatus? status)
        {
            var query = _context.TaskItems
                .Include(t => t.CreatedBy)
                .Include(t => t.AssignedUserId)
                .AsQueryable();

            if (workType.HasValue)
                query = query.Where(t => t.WorkType == workType.Value);

            if (createdById.HasValue)
                query = query.Where(t => t.CreatedById == createdById.Value);
            
            if (assignedUserId.HasValue)
                query = query.Where(t => t.AssignedUserId == assignedUserId.Value);

            if (status.HasValue)
                query = query.Where(t => t.Status == status.Value);

            return await query.ToListAsync();
        }

    }
}
