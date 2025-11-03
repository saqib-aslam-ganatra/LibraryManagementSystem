using LibraryManagement.Application.Features.Members.DTOs;
using MediatR;

namespace LibraryManagement.Application.Features.Members.Commands.UpdateMember
{
    public class UpdateMemberCommand : IRequest<MemberDto>
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
