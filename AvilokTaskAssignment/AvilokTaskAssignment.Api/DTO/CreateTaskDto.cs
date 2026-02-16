using AvilokTaskAssignment.Data.Models;

namespace AvilokTaskAssignment.Api.DTO
{
    public class CreateTaskDto
    {
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public WorkType WorkType { get; set; }
        public DateTime Deadline { get; set; }
    }
}
