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

namespace Application.Features.TeamsHero.Commands.CreateHeroTeamCommand
{
    public class CreateHeroOnTeamCommand : IRequest<Response<int>>
    {
        public int IdHero { get; set; }
        public int IdTeam { get; set; }
    }

    public class CreateTeamsCommandHandler : IRequestHandler<CreateHeroOnTeamCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Teams_Hero> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateTeamsCommandHandler(IRepositoryAsync<Teams_Hero> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateHeroOnTeamCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var nuevoRegistro = _mapper.Map<Teams_Hero>(request);

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
