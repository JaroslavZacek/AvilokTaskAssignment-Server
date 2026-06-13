using System;
using System.Collections.Generic;
using System.Text;

namespace AvilokTaskAssignment.Data.Models
{
    public class TaskComent
    {
        
        public Guid Id { get; set; }

        // -----------------
        // Vztah k zakázce
        // -----------------
        public Guid TaskId { get; set; }
        public TaskItem Task { get; set; } = null;

        // -----------------
        // Autor komentáře
        // -----------------
        public Guid AuthorId { get; set; }
        public ApplicationUser Author { get; set; } = null;

        public string Text { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
