using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.Teams.Commands.DeleteTeamsCommand
{
    public class DeleteTeamsCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }
    public class DeleteTeamsCommandHandler : IRequestHandler<DeleteTeamsCommand, Response<int>>
    {
        private readonly IRepositoryAsync<TeamsRescue> _repositoryAsync;
        public DeleteTeamsCommandHandler(IRepositoryAsync<TeamsRescue> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(DeleteTeamsCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var Teams = await _repositoryAsync.GetByIdAsync(request.Id);
                if (Teams == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {
                    await _repositoryAsync.DeleteAsync(Teams);
                    return new Response<int>(Teams.Id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }

        }
    }
}
