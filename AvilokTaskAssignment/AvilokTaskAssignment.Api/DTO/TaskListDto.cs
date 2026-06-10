using AvilokTaskAssignment.Data.Models;

using TaskStatus = AvilokTaskAssignment.Data.Models.TaskStatus;

namespace AvilokTaskAssignment.Api.DTO
{
    public class TaskListDto
    {
        /// <summary>
        /// Id zakázky.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Krátký popis zakázky.
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// Typ práce (Development, Graphic, Story).
        /// </summary>
        public WorkType WorkType { get; set; }

        /// <summary>
        /// Aktuální stav zakázky.
        /// </summary>
        public TaskStatus Status { get; set; }

        /// <summary>
        /// Datum, do kterého musí být zakázka dokončena.
        /// </summary>
        public DateTime Deadline{ get; set; }

        /// <summary>
        /// Jméno uživatele, který zakázku vytvořil.
        /// </summary>
        public string CreatedByName { get; set; }

        /// <summary>
        /// Jméno přiřazeného uživatele, pokud je zakázka přiřazena.
        /// </summary>
        public string? AssignedUserName { get; set; }

        /// <summary>
        /// Indikuje, zda je zakázka po termínu.
        /// </summary>
        public bool isOverdue {  get; set; }
    }
}
