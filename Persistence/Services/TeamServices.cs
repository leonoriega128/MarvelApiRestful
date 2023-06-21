using Application.DTOs;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class TeamServices : ITeamServices
    {
        private readonly ApplicationDbContext _context;

        public TeamServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool IsCaptured(string Name)
        {
            if (Name.Contains("Iron Man") || Name.Contains("Captain America") ||
                Name.Contains("Captain Marvel") || Name.Contains("Hulk") || Name.Contains("Black Panther"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<List<CharacterHeroDTO?>> SearchCharactersByName(string characters)
        {
            var query = from c in _context.CharacterHeroes
                        where c.Name.Contains(characters) && !c.Captured
                        select new CharacterHeroDTO
                        {
                            Name = c.Name,
                            Description = c.Description,
                            UrlImage = c.UrlImage,
                            ModifiedDate = c.ModifiedDate,
                            Intelligence = c.Intelligence,
                            Agility = c.Agility,
                            Force = c.Force,
                            Id = c.Id
                        };
            var result = await query.ToListAsync();
            return result;
        }

        public async Task<List<CharacterHeroDTO?>> SearchByID(int characters)
        {
            var query = from c in _context.CharacterHeroes
                        where c.Id == characters && !c.Captured
                        select new CharacterHeroDTO
                        {
                            Name = c.Name
                        };
            var result = await query.ToListAsync();
            if (result.Count > 0)
            {
                if (VillainFilter(result.FirstOrDefault().Name))
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<List<CharacterHeroDTO?>> GetAllCharacter()
        {
            var query = from c in _context.CharacterHeroes
                        where !c.Captured
                        select new CharacterHeroDTO
                        {
                            Name = c.Name,
                            Description = c.Description,
                            UrlImage = c.UrlImage,
                            ModifiedDate = c.ModifiedDate,
                            Intelligence = c.Intelligence,
                            Agility = c.Agility,
                            Force = c.Force,
                            Id = c.Id
                        };

            var result = await query.ToListAsync();

            return result;
        }


        public async Task<List<TeamsDTO?>> GetAllTeams()
        {
            try
            {
                var query = from team in _context.Teams
                            join teamHero in _context.TeamsHero on team.Id equals teamHero.IdTeam
                            join character in _context.CharacterHeroes on teamHero.IdHero equals character.Id
                            group new { team, character } by new { team.Id, team.Name } into g
                            select new TeamsDTO
                            {
                                Name = g.Key.Name,
                                NumberMembers = g.Count(),
                                ForceMember = (g.OrderByDescending(gc => gc.character.Force).FirstOrDefault().character.Name.ToString()),
                                IntelliMember = g.OrderByDescending(gc => gc.character.Intelligence).FirstOrDefault().character.Name.ToString(),
                                AgilityMember = g.OrderByDescending(gc => gc.character.Agility).FirstOrDefault().character.Name.ToString()
                            };

                var result = await query.ToListAsync();

                return result;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public async Task<int> SearchByIDTeamIDHero(int idHero, int idTeam)
        {
            try
            {
                var query = from c in _context.TeamsHero
                            where c.IdHero == idHero && c.IdTeam == idTeam
                            select new TeamHeroDTO
                            {
                                Id = c.Id
                            };

                var result = await query.FirstOrDefaultAsync();

                if (result != null)
                {
                    return result.Id;
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }
        public string GetMD5Hash(string input)
        {
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                var hashBytes = md5.ComputeHash(inputBytes);

                var sb = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString().ToLower();
            }
        }


        public bool VillainFilter(string villan)
        {
            if (villan.Contains("Thanos") || villan.Contains("Loki") || villan.Contains("Ultron") || villan.Contains("Red Skull"))
            {
                return false;

            }
            else
            {
                return true;
            }
        }


        public async Task<List<CharacterHeroDTO?>> SearchVillians()
        {
            var query = from c in _context.CharacterHeroes
                        where c.Name.Contains("The Collector")
                        && !c.Captured
                        select new CharacterHeroDTO
                        {
                            Name = c.Name,
                            Description = c.Description,
                            UrlImage = c.UrlImage,
                            MarvelID = c.MarvelID,

                        };
            var result = await query.ToListAsync();
            return result;
        }


        public string getUrl(string opcion)
        {
            var publicKey = "555f06eb9d76cc0ee29e880bb815014a";
            var privateKey = "818aa04060ee52f3441103f9daa9ba321f224227";
            var timestamp = 2;
            var hash = GetMD5Hash(timestamp + privateKey + publicKey);
            if (opcion.Equals("CharacterByid"))
            {
                var url = $"https://gateway.marvel.com:443/v1/public/characters/1012080?ts={timestamp}&apikey={publicKey}&hash={hash}";
                return url;
            }
            else
            {
                var url = $"https://gateway.marvel.com:443/v1/public/characters/1012080/stories?ts={timestamp}&apikey={publicKey}&hash={hash}";
                return url;
            }
        }

    }
    
}
