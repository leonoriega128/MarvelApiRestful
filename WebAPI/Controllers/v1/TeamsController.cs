using Application.Features.Teams.Commands.CreateTeamsCommand;
using Application.Features.Teams.Commands.DeleteTeamsCommand;
using Application.Features.Teams.Commands.UpdateTeamsCommand;
using Application.Features.TeamsHero.Commands.CreateHeroTeamCommand;
using Application.Features.TeamsHero.Commands.DeleteTeamsHeroCommand;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Persistence.Interfaces;

namespace WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class TeamsController : BaseApiController
    {
        private readonly ITeamServices _teamServices;

        public TeamsController(ITeamServices context)
        {
            _teamServices = context;
        }

        /// <summary>
        /// Busca heroes que hayann sido cargados en la base para los que contengan algun parecido con el nombre
        /// </summary>
        /// <param name="characters"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCharactersByName")]
        public async Task<IActionResult> GetCharacter(string characters)
        {
            if (characters == null)
            {
                return BadRequest("Debe ingresar el nombre de un heroe para realizar la busqueda");
            }
            else
            {
                var response = await _teamServices.SearchCharactersByName(characters);
                return Ok(response);
            }
            
        }

        /// <summary>
        /// Trae todos los heroes que hayan sido cargados en la base de datos
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAllCharacters")]
        public async Task<IActionResult> GetAllCharacters()
        {
            var response = await _teamServices.GetAllCharacter();
            return Ok(response);
        }

        /// <summary>
        /// Crea un equipo nuevo de super heroes y si se lo desea agrega los integrantes del equipo, 
        /// asignandole id de heroe de cada uno
        /// </summary>
        /// <param name="createNameRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateName")]
        public async Task<IActionResult> CreateName(CreateNameRequest createNameRequest)
        {
            var command = new CreateTeamsCommand();
            var commandHeros = new CreateHeroOnTeamCommand();
            try
            {
              command.Name = createNameRequest.teamname;
              var response=  await Mediator.Send(command);
                if(createNameRequest != null)
                {
                    var data = response.Data;
                    foreach (var hero in createNameRequest.HeroList)
                    {
                        foreach (var item in hero.idHero)
                        {
                            var resp = item;
                            var question = await _teamServices.SearchByID(int.Parse(resp.ToString()));
                            
                            if (question != null)
                            {
                                commandHeros.IdTeam = data;
                                commandHeros.IdHero = int.Parse(resp.ToString());
                                response = await Mediator.Send(commandHeros);
                            }
                        }
                    }
                   
                }
                
              return Ok(response);
            }
            catch (Exception ex)
            {
                Console.Write(ex.ToString());
                return BadRequest(ex.ToString());
            }
        }

        /// <summary>
        /// muestra los equipos de rescate con los integrantes y sus atributos mas fuertes 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("RescueTeam")]
        public async Task<IActionResult> RescueTeam()
        {
            var response = await _teamServices.GetAllTeams();
            return Ok(response);
        }

        /// <summary>
        /// modifica el nombre del equipo
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateTeamsCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// elimina el equipo 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("Team/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteTeamsCommand { Id = id }));
        }

        /// <summary>
        /// elimina un integrante del equipo
        /// </summary>
        /// <param name="idHero"></param>
        /// <param name="idTeam"></param>
        /// <returns></returns>
        [HttpDelete("HeroIntoTeam/{idTeam}/{idHero}")]
        public async Task<IActionResult> HeroIntoTeam(int idHero, int idTeam)
        {
            var question = await _teamServices.SearchByIDTeamIDHero(idHero, idTeam);
            if (question == 0)
            {
                return BadRequest("El heroe no pertenece al equipo ingresado");
            }
            else
            {
                return Ok(await Mediator.Send(new DeleteTeamsHeroCommand { Id = question }));
            }
        }

    }
}
