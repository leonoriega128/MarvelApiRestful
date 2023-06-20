using Application.DTOs;
using Application.Features.CharactersHero.Commands.CreateCharactersCommand;
using Application.Features.Teams.Commands.CreateTeamsCommand;
using Application.Features.TeamsHero.Commands.CreateHeroTeamCommand;
using AutoMapper;
using Domain.Entities;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            #region DTOs
            CreateMap<CharacterHero,CharacterHeroDTO>();
            CreateMap<TeamsRescue, TeamsDTO>();
            CreateMap<Teams_Hero, TeamHeroDTO>();
            #endregion
            #region Commands
            CreateMap<CreateTeamsCommand, TeamsRescue>();
            CreateMap<CreateCharacterCommand, CharacterHero>();
            CreateMap<CreateHeroOnTeamCommand, Teams_Hero>();
            #endregion
        }
    }
}
