using Application.Features.CharactersHero.Commands.CreateCharactersCommand;
using Application.Features.CharactersHero.Commands.DeleteCharacterCommand;
using Application.Features.CharactersHero.Commands.UpdateCharacterCommand;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Persistence.Services;
using System.Text;
using System.Text.Json;

namespace WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class CharacterHeroController : BaseApiController
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly TeamServices _teamServices;


        public CharacterHeroController(IHttpClientFactory clientFactory,TeamServices context)
        {
            _clientFactory = clientFactory;
            _teamServices = context;
        }

       
        /// <summary>
        /// Se recibe el nombre del heroe y controla si son mayores a 4 caracteres, si es asi se conecta con la api para 
        /// traer informacion del heroe y guardarlos en la tabla "characters"
        /// </summary>
        /// <param name="characters"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetCharacter")]
        public async Task<IActionResult> GetCharacter(string characters)
        {
            var command = new CreateCharacterCommand();
            string charAux = "";
            Random random = new Random();
            if (characters.Length >= 4)
            {
               
                for (int i = 0; i < characters.Length; i++)
                {
                    charAux = charAux + characters[i];
                }

                var client = _clientFactory.CreateClient();
                var publicKey = "555f06eb9d76cc0ee29e880bb815014a";
                var privateKey = "818aa04060ee52f3441103f9daa9ba321f224227";
                var timestamp = 1;
                var hash = _teamServices.GetMD5Hash(timestamp + privateKey + publicKey);

                var url = $"https://gateway.marvel.com/v1/public/characters?nameStartsWith={charAux}&ts={timestamp}&apikey={publicKey}&hash={hash}";

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    using var responseStream = await response.Content.ReadAsStreamAsync();
                    var jsonOptions = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true,
                    };
                    var characterInfo = await JsonSerializer.DeserializeAsync<MarvelCharacterDataWrapper>(responseStream, jsonOptions);
                    var avengersCharacters = characterInfo.Data.Results
                             .Where(c => c.Series.Items.Any(s => s.Name.Contains("Avengers")));

                    foreach ( var character in avengersCharacters )
                    {

                        command.MarvelID = character.id;
                        command.Name = character.Name;
                        command.Description = character.Description?.Substring(0, Math.Min(character.Description.Length, 250)) ?? "Sin descripción";
                        command.UrlImage = character.thumbnail.path + "." + character.thumbnail.extension ;
                        command.Intelligence = random.Next(1, 101);
                        command.Force = random.Next(1, 101);
                        command.Agility = random.Next(1, 101);
                        command.ModifiedDate = DateTime.Parse(character.modified ?? "");
                        command.Captured = _teamServices.IsCaptured(character.Name);
                       
                        try
                        {

                        await Mediator.Send(command);

                        }catch (Exception ex)
                        {
                            Console.Write(ex.ToString());
                        }
                    }
                    return Ok(avengersCharacters);
                }
                else
                {
                    return StatusCode((int)response.StatusCode);
                }
            }
            else
            {
                return BadRequest("El nombre del personaje debe tener al menos 4 caracteres");
            }
        }

          /// <summary>
          /// Endpoint para modificar datos del heroe en la tabla Character
          /// </summary>
          /// <param name="id"></param>
          /// <param name="command"></param>
          /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateCharacterCommand command)
        {
            if (id != command.Id)
                return BadRequest();

            return Ok(await Mediator.Send(command));
        }

        /// <summary>
        /// Endpoint para eliminar heroe por id en la tabla Character
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteCharacterCommand { Id = id }));
        }

     
    }
  
}
