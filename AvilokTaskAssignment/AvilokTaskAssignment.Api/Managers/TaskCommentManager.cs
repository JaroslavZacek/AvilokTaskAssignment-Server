using AvilokTaskAssignment.Api.DTO;
using AvilokTaskAssignment.Api.Interfaces;
using AvilokTaskAssignment.Data.Interfaces;
using AvilokTaskAssignment.Data.Models;

namespace AvilokTaskAssignment.Api.Managers
{
    public class TaskCommentManager : ITaskCommentManager
    {
        private readonly ITaskCommentRepository _commentRepository;

        public TaskCommentManager (ITaskCommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
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
        public async Task AddCommentAsync(Guid taskId, Guid authorId, string text)
        {
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
    }
}
