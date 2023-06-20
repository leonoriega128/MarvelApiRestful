using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.TeamsHero.Commands.DeleteTeamsHeroCommand
{
    public class DeleteTeamsHeroCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }
    public class DeleteTeamsHeroCommandHandler : IRequestHandler<DeleteTeamsHeroCommand, Response<int>>
    {
        private readonly IRepositoryAsync<Teams_Hero> _repositoryAsync;
        public DeleteTeamsHeroCommandHandler(IRepositoryAsync<Teams_Hero> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(DeleteTeamsHeroCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var TeamsHero = await _repositoryAsync.GetByIdAsync(request.Id);
                if (TeamsHero == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {
                    await _repositoryAsync.DeleteAsync(TeamsHero);
                    return new Response<int>(TeamsHero.Id);
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
