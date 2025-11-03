using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Api.Models.Members
{
    public class MemberCreateRequest
    {
        [Required]
        [StringLength(200)]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Phone]
        public string? PhoneNumber { get; set; }

        [StringLength(400)]
        public string? Address { get; set; }
    }
}
