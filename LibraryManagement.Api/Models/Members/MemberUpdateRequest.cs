using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Api.Models.Members
{
    public class MemberUpdateRequest : MemberCreateRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
    }
}
