using AvilokTaskAssignment.Data.Models;

namespace AvilokTaskAssignment.Api.DTO
{
    public class TaskFilterDto
    {
        public WorkType? WorkType { get; set; }
        public Guid? CreatedById { get; set; }
        public Guid? AssignedUserId { get; set; }
        public Data.Models.TaskStatus? Status { get; set; }
    }
}
