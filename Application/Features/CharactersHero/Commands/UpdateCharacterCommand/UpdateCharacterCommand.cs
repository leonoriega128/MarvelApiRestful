using Application.Interfaces;
using Application.Wrappers;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.CharactersHero.Commands.UpdateCharacterCommand
{
    public class UpdateCharacterCommand : IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public MarvelUrlImage? thumbnail { get; set; }
        public string? modified { get; set; }
        public int Intelligence { get; set; }
        public int Agility { get; set; }
        public int Force { get; set; }
    }
    public class UpdateCharacterCommandHandler : IRequestHandler<UpdateCharacterCommand, Response<int>>
    {
        private readonly IRepositoryAsync<CharacterHero> _repositoryAsync;
        private readonly IMapper _mapper;
        public UpdateCharacterCommandHandler(IRepositoryAsync<CharacterHero> repositoryAsync, IMapper mapper)
        {
            _repositoryAsync = repositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(UpdateCharacterCommand request, CancellationToken cancellationToken)
        {
            var CharacterHero = await _repositoryAsync.GetByIdAsync(request.Id);
            if (CharacterHero == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {
                try { 
                CharacterHero.Name = request.Name;
                CharacterHero.Description = request.Description;
                CharacterHero.UrlImage = request.thumbnail.path;
                CharacterHero.ModifiedDate = DateTime.Parse(request.modified ?? "");
                }catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
                await _repositoryAsync.UpdateAsync(CharacterHero);
                return new Response<int>(CharacterHero.Id);
            }
        }
    }
}
