namespace AvilokTaskAssignment.Api.DTO
{
    public class CommentDto
    {
        public Guid Id { get; set; }

        // ----------------------------
        // Informace o Autorovi komentáře
        // ----------------------------
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; } = string.Empty;

        public string Text { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }
    }
}
