using System;
using System.Collections.Generic;
using System.Text;

namespace AvilokTaskAssignment.Data.Models
{
    public class TaskComment
    {
        /// <summary>
        /// Unikátní identifikátor komentáře.
        /// </summary>
        public Guid Id { get; set; }

        // --------------------------
        // Zakázka
        // --------------------------

        /// <summary>
        /// Identifikátor úkolu (cizí klíč), ke kterému komentář patří.
        /// </summary>
        public Guid TaskId { get; set; }

        /// <summary>
        /// Navigační vlastnost na související úkol.
        /// </summary>
        public TaskItem Task { get; set; } = null!;

        // --------------------------
        // Autor
        // --------------------------

        /// <summary>
        /// Identifikátor uživatele (autora) komentáře.
        /// </summary>
        public Guid AuthorId { get; set; }

        /// <summary>
        /// Navigační vlastnost na autora komentáře.
        /// </summary>
        public ApplicationUser Author { get; set; } = null!;

        /// <summary>
        /// Text komentáře.
        /// </summary>
        public string Text { get; set; } = string.Empty;

        /// <summary>
        /// Datum a čas vytvoření komentáře.
        /// </summary>
        public DateTime CreatedAt { get; set; }

    }
}
