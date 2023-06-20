using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Interfaces
{
    public interface ITeamServices
    {

        public bool IsCaptured(string Name);   

        public Task<List<CharacterHeroDTO?>> SearchCharactersByName(string characters);
        
        public Task<List<CharacterHeroDTO?>> GetAllCharacter();

        public Task<List<CharacterHeroDTO?>> SearchByID(int characters);
        public Task<List<TeamsDTO?>> GetAllTeams();
        public Task<int> SearchByIDTeamIDHero(int idHero, int idTeam);

        }
}
