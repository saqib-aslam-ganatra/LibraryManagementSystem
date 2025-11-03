using AutoMapper;
using LibraryManagement.Application.Common.Interfaces;
using LibraryManagement.Application.Features.Authors.DTOs;
using LibraryManagement.Domain.Entities;
using MediatR;

namespace LibraryManagement.Application.Features.Authors.Commands.CreateAuthor
{
    public class CreateAuthorCommandHandler : IRequestHandler<CreateAuthorCommand, AuthorDto>
    {
        private readonly IAuthorRepository _repository;
        private readonly IMapper _mapper;

        public CreateAuthorCommandHandler(IAuthorRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<AuthorDto> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = new Domain.Entities.Author
            {
                Name = request.Name,
                Biography = request.Biography
            };

            var created = await _repository.AddAsync(author);
            return _mapper.Map<AuthorDto>(created);
        }
    }
}
