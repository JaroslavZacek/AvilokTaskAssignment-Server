using AvilokTaskAssignment.Api.DTO;
using AvilokTaskAssignment.Api.Helpers;
using AvilokTaskAssignment.Api.Interfaces;
using AvilokTaskAssignment.Data.Interfaces;
using AvilokTaskAssignment.Data.Models;

namespace AvilokTaskAssignment.Api.Managers
{
    public class TaskCommentManager : ITaskCommentManager
    {
        private readonly ITaskCommentRepository _commentRepository;
        private readonly ITaskRepository _taskRepository;

        public TaskCommentManager (ITaskCommentRepository commentRepository, ITaskRepository taskRepository)
        {
            _commentRepository = commentRepository;
            _taskRepository = taskRepository;
        }

        #region Get
        /// <summary>
        /// Vrátí všechny komentáře k danému úkolu.
        /// </summary>
        public async Task<IEnumerable<CommentDto>> GetCommentsAsync(Guid taskId)
        {
            var comments = await _commentRepository.GetByTaskIdAsync(taskId);

            return comments.Select(c => new CommentDto
            {
                Id = c.Id,
                AuthorId = c.AuthorId,
                AuthorName = c.Author.FullName,
                Text = c.Text,
                CreatedAt = c.CreatedAt
            });
        }
    
        #endregion

        #region Post
        /// <summary>
        /// Přidá nový komentář k úkolu.
        /// </summary>
        public async Task AddCommentAsync(Guid taskId, Guid authorId, string text, List<string> roles)
        {
            var task = await _taskRepository.GetByIdAsync(taskId);

            if (task == null)
                throw new Exception("Zakázka nebyla nalezena.");

            PermissionHelper.EnsureTaskLeaderAccess(roles, task.WorkType, "Nemáte oprávnění přidávat komentáře k této zakázce.");

            var comment = new TaskComment
            {
                Id = Guid.NewGuid(),
                TaskId = taskId,
                AuthorId = authorId,
                Text = text,
                CreatedAt = DateTime.UtcNow
            };

            await _commentRepository.AddAsync(comment);

            await _commentRepository.SaveChangesAsync();
        }
        #endregion

        #region Path

        /// <summary>
        /// Metoda upravující komentář.
        /// </summary>
        public async Task UpdateCommentAsync(Guid commentId, string text, List<string> roles)
        {
            var comment = await _commentRepository.GetWithTaskAsync(commentId);

            if (comment == null)
                throw new Exception("Komentář nebyl nalezen.");

            PermissionHelper.EnsureTaskLeaderAccess(roles, comment.Task.WorkType, "Nemáte oprávnění upravi tento komentář.");

            comment.Text = text;

            _commentRepository.Update(comment);

            await _commentRepository.SaveChangesAsync();
        }

        #endregion

        #region Delete
        /// <summary>
        /// Vymaže komentář, pokud je přihlášená osoba admin nebo leader pro danou zakázku.
        /// </summary>
        public async Task DeleteCommentAsync(Guid commentId, List<string> roles)
        {
            var comment = await _commentRepository.GetWithTaskAsync(commentId);

            if (comment == null)
                throw new Exception("Komentář nebyl nalezen.");

            PermissionHelper.EnsureTaskLeaderAccess(roles, comment.Task.WorkType, "Nemáte oprávnění smazat tento komentář.");

            _commentRepository.Remove(comment);

            await _commentRepository.SaveChangesAsync();
        }

        #endregion
    }
}
