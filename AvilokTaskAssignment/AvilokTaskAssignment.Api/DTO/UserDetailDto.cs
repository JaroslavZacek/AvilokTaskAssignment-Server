namespace AvilokTaskAssignment.Api.DTO
{
    public class UserDetailDto
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public List<string> Roles { get; set; } = [];
    }
}
