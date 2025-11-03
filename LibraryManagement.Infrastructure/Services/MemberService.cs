using AutoMapper;
using LibraryManagement.Application.Common.Interfaces;
using LibraryManagement.Application.Features.Members.DTOs;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Infrastructure.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _repository;
        private readonly IMapper _mapper;

        public MemberService(IMemberRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MemberDto>> GetAllAsync()
        {
            var members = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<MemberDto>>(members);
        }

        public async Task<MemberDto?> GetByIdAsync(int id)
        {
            var member = await _repository.GetByIdAsync(id);
            return _mapper.Map<MemberDto>(member);
        }

        public async Task<MemberDto> CreateAsync(MemberDto dto)
        {
            var entity = _mapper.Map<Member>(dto);
            var created = await _repository.AddAsync(entity);
            return _mapper.Map<MemberDto>(created);
        }

        public async Task<MemberDto> UpdateAsync(MemberDto dto)
        {
            var entity = _mapper.Map<Member>(dto);
            var updated = await _repository.UpdateAsync(entity);
            return _mapper.Map<MemberDto>(updated);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
