using AutoMapper;
using LibraryManagement.Application.Common.Interfaces;
using LibraryManagement.Application.Features.Members.DTOs;
using MediatR;

namespace LibraryManagement.Application.Features.Members.Commands.CreateMember
{
    public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, MemberDto>
    {
        private readonly IMemberRepository _repository;
        private readonly IMapper _mapper;

        public CreateMemberCommandHandler(IMemberRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MemberDto> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            var member = new LibraryManagement.Domain.Entities.Member
            {
                FullName = request.FullName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                JoinedDate = DateTime.UtcNow
            };

            var created = await _repository.AddAsync(member);
            return _mapper.Map<MemberDto>(created);
        }
    }
}
