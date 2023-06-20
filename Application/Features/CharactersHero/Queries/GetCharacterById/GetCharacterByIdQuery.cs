using Application.DTOs;
using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CharactersHero.Queries.GetClienteById
{
    public class GetCharacterByIdQuery : IRequest<Response<CharacterHeroDTO>>
    {
        public int Id { get; set; }
        public class GetCharacterByIdQueryHandler : IRequestHandler<GetCharacterByIdQuery, Response<CharacterHeroDTO>>
        {
            private readonly IRepositoryAsync<CharacterHeroDTO> _repositoryAsync;

            private readonly IMapper _mapper;
            public GetCharacterByIdQueryHandler(IRepositoryAsync<CharacterHeroDTO> repositoryAsync, IMapper mapper)
            {
                _repositoryAsync = repositoryAsync;
                _mapper = mapper;
            }

            public async Task<Response<CharacterHeroDTO>> Handle(GetCharacterByIdQuery request, CancellationToken cancellationToken)
            {
                var chracterh = await _repositoryAsync.GetByIdAsync(request.Id);
                if (chracterh == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {
                    var dto = _mapper.Map<CharacterHeroDTO>(chracterh);
                    return new Response<CharacterHeroDTO>(dto);
                }


            }
        }
    }
}
