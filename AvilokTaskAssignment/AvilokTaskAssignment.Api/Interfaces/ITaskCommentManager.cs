using AvilokTaskAssignment.Api.DTO;

namespace AvilokTaskAssignment.Api.Interfaces
{
    public interface ITaskCommentManager
    {
        Task<IEnumerable<CommentDto>> GetCommentsAsync(Guid taskId);

        Task AddCommentAsync(Guid taskId, Guid authorId, string text, List<string> roles);

        Task DeleteCommentAsync(Guid commentId, List<string> roles);
    }
}
