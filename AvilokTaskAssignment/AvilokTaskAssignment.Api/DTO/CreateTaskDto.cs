using AvilokTaskAssignment.Data.Models;

namespace AvilokTaskAssignment.Api.DTO
{
    public class CreateTaskDto
    {
        /// <summary>
        /// Krátký popis zakázky.
        /// </summary>
        public string ShortDescription { get; set; }
        /// <summary>
        /// Detailní popis zakázky, který může obsahovat více informací o požadavcích, očekáváních a dalších detailech potřebných pro úspěšné dokončení úkolu.
        /// </summary>
        public string LongDescription { get; set; }
        /// <summary>
        /// Typ práce.
        /// </summary>
        public WorkType WorkType { get; set; }
        /// <summary>
        /// Dedline zakázky.
        /// </summary>
        public DateTime Deadline { get; set; }
    }
}
