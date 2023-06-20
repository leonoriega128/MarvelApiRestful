using Application.Interfaces;
using Application.Wrappers;
using Domain.Entities;
using MediatR;

namespace Application.Features.CharactersHero.Commands.DeleteCharacterCommand
{
    public class DeleteCharacterCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
    }
    public class DeleteCharacterCommandHandler : IRequestHandler<DeleteCharacterCommand, Response<int>>
    {
        private readonly IRepositoryAsync<CharacterHero> _repositoryAsync;
        public DeleteCharacterCommandHandler(IRepositoryAsync<CharacterHero> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task<Response<int>> Handle(DeleteCharacterCommand request, CancellationToken cancellationToken)
        {
            try
            {

                var CharacterHero = await _repositoryAsync.GetByIdAsync(request.Id);
                if (CharacterHero == null)
                {
                    throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
                }
                else
                {
                    await _repositoryAsync.DeleteAsync(CharacterHero);
                    return new Response<int>(CharacterHero.Id);
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
