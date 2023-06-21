using Application.DTOs;

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
        public string GetMD5Hash(string input);
        public Task<List<CharacterHeroDTO?>> SearchVillians();
        public bool VillainFilter(string villan);
        public string getUrl(string opcion);


        }
}
