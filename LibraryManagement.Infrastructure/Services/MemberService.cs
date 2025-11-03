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
            return member is null ? null : _mapper.Map<MemberDto>(member);
        }

        public async Task<MemberDto> CreateAsync(MemberDto dto)
        {
            var entity = _mapper.Map<Member>(dto);
            entity.Address ??= string.Empty;
            entity.PhoneNumber ??= string.Empty;
            var created = await _repository.AddAsync(entity);
            return _mapper.Map<MemberDto>(created);
        }

        public async Task<bool> UpdateAsync(MemberDto dto)
        {
            var entity = await _repository.GetByIdAsync(dto.Id);
            if (entity is null)
            {
                return false;
            }

            entity.FullName = dto.FullName;
            entity.Email = dto.Email;
            entity.PhoneNumber = dto.PhoneNumber ?? string.Empty;
            entity.Address = dto.Address ?? string.Empty;

            await _repository.UpdateAsync(entity);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
