using AvilokTaskAssignment.Data.Models;

namespace AvilokTaskAssignment.Api.DTO
{
    public class UpdateTaskDto
    {
        public string ShortDescription { get; set; } = string.Empty;

        public string LongDescription {  get; set; } = string.Empty;

        public WorkType WorkType {  get; set; }

        public DateTime Deadline { get; set; }
    }
}
