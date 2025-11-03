using AutoMapper;
using LibraryManagement.Application.Common.Interfaces;
using LibraryManagement.Application.Features.Members.DTOs;
using MediatR;

namespace LibraryManagement.Application.Features.Members.Queries.GetMemberById
{
    public class GetMemberByIdQueryHandler : IRequestHandler<GetMemberByIdQuery, MemberDto>
    {
        private readonly IMemberRepository _repository;
        private readonly IMapper _mapper;

        public GetMemberByIdQueryHandler(IMemberRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<MemberDto> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
        {
            var member = await _repository.GetByIdAsync(request.Id);
            return _mapper.Map<MemberDto>(member);
        }
    }
}
