using LibraryManagement.Application.Features.Members.DTOs;
using MediatR;

namespace LibraryManagement.Application.Features.Members.Queries.GetMemberById
{
    public class GetMemberByIdQuery : IRequest<MemberDto>
    {
        public int Id { get; set; }
    }
}
