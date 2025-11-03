using AutoMapper;
using LibraryManagement.Application.Common.Interfaces;
using LibraryManagement.Application.Features.Members.DTOs;
using MediatR;

namespace LibraryManagement.Application.Features.Members.Commands.UpdateMember
{
    public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, MemberDto>
    {
        private readonly IMemberRepository _repository;
        private readonly IMapper _mapper;

        public UpdateMemberCommandHandler(IMemberRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MemberDto> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
        {
            var member = await _repository.GetByIdAsync(request.Id);
            if (member == null) return null;

            member.FullName = request.FullName;
            member.Email = request.Email;
            member.PhoneNumber = request.PhoneNumber;

            await _repository.UpdateAsync(member);
            return _mapper.Map<MemberDto>(member);
        }
    }
}
