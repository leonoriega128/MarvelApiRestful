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
            if (Name.Contains("Iron Man") || Name.Contains("Capitan America") ||
                Name.Contains("Capitana Marvel")|| Name.Contains("Hulk") || Name.Contains("Pantera Negra"))
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
                        where c.Name.Contains(characters) && !c.Capturado 
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
                        where c.Id == characters && !c.Capturado
                        select new CharacterHeroDTO
                        {
                            Id = c.Id
                        };
            var result = await query.ToListAsync();
            return result;
        }

        public async Task<List<CharacterHeroDTO?>> GetAllCharacter()
        {
            var query = from c in _context.CharacterHeroes
                        where !c.Capturado
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
            try { 
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
            }catch (Exception ex)
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
    }

}
