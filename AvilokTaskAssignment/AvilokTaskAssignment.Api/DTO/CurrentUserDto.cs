namespace AvilokTaskAssignment.Api.DTO
{
    public class CurrentUserDto
    {
        public string UserId { get; set; } = string.Empty;

        public string? Email { get; set; }

        public List<string> Roles { get; set; } = [];
    }
}
