using AvilokTaskAssignment.Data.Models;

using TaskStatus = AvilokTaskAssignment.Data.Models.TaskStatus;

namespace AvilokTaskAssignment.Api.DTO
{
    public class TaskDetailDto
    {
        /// <summary>
        /// Id zakázky, které je potřeba pro zobrazení detailu zakázky a pro případné další operace s touto zakázkou (např. aktualizace stavu, přiřazení k uživateli, atd.)
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Krátký popis zakázky, který bude zobrazen v přehledu zakázek a na detailu zakázky. Tento údaj je důležitý pro rychlou orientaci v 
        /// seznamu zakázek a pro zobrazení základních informací o zakázce na detailu zakázky
        /// </summary>
        public string ShortDescription { get; set; } = string.Empty;

        /// <summary>
        /// Detailní popis zakázky, který bude zobrazen na detailu zakázky. Tento údaj je důležitý pro zobrazení všech potřebných informací o zakázce,
        /// </summary>
        public string LongDescription { get; set; } = string.Empty;

        /// <summary>
        /// Typ práce, který je potřeba pro realizaci zakázky. Tento údaj je důležitý pro případné filtrování zakázek popř. i komu tato zakázky bude nebo může být přiřazena.
        /// </summary>
        public WorkType WorkType { get; set; }

        /// <summary>
        /// Stav zakázky, který je důležitý pro zobrazení aktuálního stavu zakázky a pro případné filtrování zakázek podle stavu.
        /// </summary>
        public TaskStatus Status { get; set; }

        /// <summary>
        /// Deadline zakázky, do kdy musí být zakázka dokončena.
        /// </summary>
        public DateTime Deadline { get; set; }

        /// <summary>
        /// Id uživatele, který vytvořil zakázku. Tento údaj je důležitý pro zobrazení informací o tom, kdo zakázku vytvořil.
        /// </summary>
        public Guid CreatedByID { get; set; }

        /// <summary>
        /// Jméno uživatele, který vytvořil zakázku. Tento údaj je důležitý pro zobrazení informací o tom, kdo zakázku vytvořil.
        /// </summary>
        public string CreatedByName { get; set; }

        /// <summary>
        /// Id uživatele, kterému je zakázka přiřazena. Tento údaj je důležitý pro zobrazení informací o tom, komu je zakázka přiřazena.¨
        /// Pokud není zakázka přiřazena žádnému uživateli, bude tento údaj null.
        /// </summary>
        public Guid? AssignedUserId { get; set; }

        /// <summary>
        /// Jméno uživatele, kterému je zakázka přiřazena. Tento údaj je důležitý pro zobrazení informací o tom, komu je zakázka přiřazena.
        /// Pokud není zakázka přiřazena žádnému uživateli, bude tento údaj null.
        /// </summary>
        public string? AssignedUserName { get; set; }

        /// <summary>
        /// Indikátor, zda je zakázka po termínu. 
        /// </summary>
        public bool IsOverdue {  get; set; }
    }
}
