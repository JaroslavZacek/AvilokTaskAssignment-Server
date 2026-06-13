using System;
using System.Collections.Generic;
using System.Text;

namespace AvilokTaskAssignment.Data.Models
{
    public class TaskComent
    {
        
        public Guid Id { get; set; }

        public Guid TaskId { get; set; }

        public Guid AuthorId { get; set; }
        public ApplicationUser Author { get; set; }

        public string Text { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
