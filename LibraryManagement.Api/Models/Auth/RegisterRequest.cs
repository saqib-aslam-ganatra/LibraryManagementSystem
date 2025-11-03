using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Api.Models.Auth
{
    public class RegisterRequest
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = default!;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = default!;

        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        [Required]
        [MaxLength(100)]
        public string UserName { get; set; } = default!;

        [Required]
        [MinLength(6)]
        public string Password { get; set; } = default!;
    }
}
