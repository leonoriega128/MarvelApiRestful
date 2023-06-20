using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Teams.Commands.CreateTeamsCommand
{
    public class CreateTeamsCommand : IRequest<Response<int>>
    {
        public string? Name { get; set; }
    }

    public class CreateTeamsCommandHandler : IRequestHandler<CreateTeamsCommand, Response<int>>
    {
        private readonly IRepositoryAsync<TeamsRescue> _repositoryAsync;
        private readonly IMapper _mapper;

        public CreateTeamsCommandHandler(IRepositoryAsync<TeamsRescue> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(CreateTeamsCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var nuevoRegistro = _mapper.Map<TeamsRescue>(request);

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
