using Application.Interfaces;
using Application.Wrappers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using AutoMapper;
using System.Threading;

namespace Application.Features.CharactersHero.Commands.CreateCharactersCommand
{
    public class CreateCharacterCommand : IRequest<Response<int>>
    {

        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? UrlImage { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public int Intelligence { get; set; }
        public int Agility { get; set; }
        public int Force { get; set; }
        public bool Captured { get; set; }
        public int MarvelID { get; set; }

    }
    public class CreateCharacterCommandHandler : IRequestHandler<CreateCharacterCommand, Response<int>>
    {
        private readonly IRepositoryAsync<CharacterHero> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateCharacterCommandHandler(IRepositoryAsync<CharacterHero> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateCharacterCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var nuevoRegistro = _mapper.Map<CharacterHero>(request);

                var data = await _repositoryAsync.AddAsync(nuevoRegistro);

                return new Response<int>(data.Id);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }


        }
    }
}
