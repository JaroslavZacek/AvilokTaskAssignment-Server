using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.AspNetCore.Identity;

namespace AvilokTaskAssignment.Data.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        /// <summary>
        /// Celé jméno uživatele.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Zakázky vytvořené tímto uživatelem.
        /// </summary>
        public ICollection<TaskItem> CreatedTasks { get; set; }

        /// <summary>
        /// Zakázky, které jsou přiřazeny tomuto uživateli.
        /// </summary>
        public ICollection<TaskItem> AssignedTasks { get; set; }
    }
}
