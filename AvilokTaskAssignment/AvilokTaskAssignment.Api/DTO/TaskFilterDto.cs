using AvilokTaskAssignment.Data.Models;

using TaskStatus = AvilokTaskAssignment.Data.Models.TaskStatus;

namespace AvilokTaskAssignment.Api.DTO
{
    public class TaskFilterDto
    {
        public WorkType? WorkType { get; set; }
        public Guid? CreatedById { get; set; }
        public Guid? AssignedUserId { get; set; }
        public TaskStatus? Status { get; set; }
    }
}
