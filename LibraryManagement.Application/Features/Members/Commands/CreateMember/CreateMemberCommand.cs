using LibraryManagement.Application.Features.Members.DTOs;
using MediatR;

namespace LibraryManagement.Application.Features.Members.Commands.CreateMember
{
    public class CreateMemberCommand : IRequest<MemberDto>
    {
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
