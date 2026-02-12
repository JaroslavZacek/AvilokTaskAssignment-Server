using System;
using System.Collections.Generic;
using System.Text;

namespace AvilokTaskAssignment.Data.Models
{
    internal class TaskItem
    {
        /// <summary>
        /// Primární klíč zakázky.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Krátký popis zakázky, který bude zobrazen v přehledu zakázek.
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// Detailní popis zakázky, který bude zobrazen na detailu zakázky.
        /// </summary>
        public string LongDescription { get; set; }

        /// <summary>
        /// Typ práce, který je potřeba pro realizaci zakázky. Tento údaj bude použit pro přiřazení zakázky k uživatelům, kteří mají tento typ práce jako svou specializaci.
        /// </summary>
        public WorkType WorkType { get; set; }

        /// <summary>
        /// Stav zakázky.
        /// </summary>
        public TaskStatus Status { get; set; }

        /// <summary>
        /// Deadline zakázky, do kdy musí být zakázka dokončena.
        /// </summary>
        public DateTime Deadline { get; set; }

        // -------------------------
        // Autor
        // -------------------------

        /// <summary>
        /// Id uživatele, který vytvořil zakázku. Tento údaj bude použit pro zobrazení informací o tom, kdo zakázku vytvořil, a pro případné filtrování zakázek podle tvůrce.
        /// </summary>
        public Guid CreatedById { get; set; }

        /// <summary>
        /// Navigační vlastnost na autora zakázky.
        /// </summary>
        public ApplicationUser CreatedBy { get; set; }

        // -------------------------
        // Přiřazený uživatel
        // -------------------------

        /// <summary>
        /// Id uživatle, kterému je zakázka přiřazena. Tento údaj bude použit pro zobrazení informací o tom, komu je zakázka přiřazena, 
        /// a pro případné filtrování zakázek podle přiřazeného uživatele.
        /// Nullable, protože zakázka nemusí být přiřazena žádnému uživateli. Pokud je zakázka přiřazena, musí být tento údaj vyplněn.
        /// </summary>
        public Guid? AssignedUserId { get; set; }

        /// <summary>
        /// Uživatel, kterému je zakázka přiřazena.
        /// </summary>
        public ApplicationUser AssignedUser { get; set; }
    }
}
