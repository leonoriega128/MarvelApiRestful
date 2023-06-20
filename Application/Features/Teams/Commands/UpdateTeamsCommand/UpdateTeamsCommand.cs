using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Teams.Commands.UpdateTeamsCommand
{
    public class UpdateTeamsCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
    public class UpdateTeamsCommandHandler : IRequestHandler<UpdateTeamsCommand, Response<int>>
    {
        private readonly IRepositoryAsync<TeamsRescue> _repositoryAsync;
        private readonly IMapper _mapper;
        public UpdateTeamsCommandHandler(IRepositoryAsync<TeamsRescue> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdateTeamsCommand request, CancellationToken cancellationToken)
        {
            var Teams = await _repositoryAsync.GetByIdAsync(request.Id);
            if (Teams == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {
                try {
                    Teams.Name = request.Name;
                }catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                await _repositoryAsync.UpdateAsync(Teams);
                return new Response<int>(Teams.Id);
            }
        }
    }
}
