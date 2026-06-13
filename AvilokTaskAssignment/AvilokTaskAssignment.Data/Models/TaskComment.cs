using System;
using System.Collections.Generic;
using System.Text;

namespace AvilokTaskAssignment.Data.Models
{
    public class TaskComment
    {
        public Guid Id { get; set; }

        // --------------------------
        // Zakázka
        // --------------------------
        public Guid TaskId { get; set; }
        public TaskItem Task { get; set; } = null!;

        // --------------------------
        // Autor
        // --------------------------
        public Guid AuthorId { get; set; }
        public ApplicationUser Author { get; set; } = null!;

        public string Text { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

    }
}
