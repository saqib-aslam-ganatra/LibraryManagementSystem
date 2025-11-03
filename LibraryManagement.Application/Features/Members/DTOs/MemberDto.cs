namespace LibraryManagement.Application.Features.Members.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public DateTime JoinedDate { get; set; }
    }
}
