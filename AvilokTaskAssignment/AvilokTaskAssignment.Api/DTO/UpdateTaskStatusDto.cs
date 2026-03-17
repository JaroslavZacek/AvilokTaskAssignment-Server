using AvilokTaskAssignment.Data.Models;
using TaskStatus = AvilokTaskAssignment.Data.Models.TaskStatus;

namespace AvilokTaskAssignment.Api.DTO
{
    public class UpdateTaskStatusDto
    {
        public TaskStatus Status { get; set; }
    }
}
