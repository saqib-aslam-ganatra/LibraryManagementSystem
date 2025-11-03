using LibraryManagement.Application.Common.Interfaces;
using MediatR;

namespace LibraryManagement.Application.Features.Members.Commands.DeleteMember
{
    public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, bool>
    {
        private readonly IMemberRepository _repository;

        public DeleteMemberCommandHandler(IMemberRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
        {
            var existing = await _repository.GetByIdAsync(request.Id);
            if (existing is null)
                return false;

            await _repository.DeleteAsync(request.Id);
            return true;
        }
    }
}
