using MediatR;

namespace LibraryManagement.Application.Features.Members.Commands.DeleteMember
{
    public class DeleteMemberCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
