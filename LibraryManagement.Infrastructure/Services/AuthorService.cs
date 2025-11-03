using AutoMapper;
using LibraryManagement.Application.Common.Interfaces;
using LibraryManagement.Application.Features.Author.DTOs;
using LibraryManagement.Domain.Entities;

namespace LibraryManagement.Infrastructure.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _repository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AuthorDto>> GetAllAuthorsAsync()
        {
            var authors = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<AuthorDto>>(authors);
        }

        public async Task<AuthorDto?> GetAuthorByIdAsync(int id)
        {
            var author = await _repository.GetByIdAsync(id);
            return _mapper.Map<AuthorDto?>(author);
        }

        public async Task<AuthorDto> CreateAuthorAsync(AuthorDto authorDto)
        {
            var entity = _mapper.Map<Author>(authorDto);
            var created = await _repository.AddAsync(entity);
            return _mapper.Map<AuthorDto>(created);
        }

        public async Task<bool> UpdateAuthorAsync(AuthorDto authorDto)
        {
            var author = await _repository.GetByIdAsync(authorDto.Id);
            if (author is null)
                return false;

            _mapper.Map(authorDto, author);
            await _repository.UpdateAsync(author);
            return true;
        }

        public async Task<bool> DeleteAuthorAsync(int id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing is null)
                return false;

            await _repository.DeleteAsync(id);
            return true;
        }
    }
}
