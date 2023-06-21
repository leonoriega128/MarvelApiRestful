using Application.Features.CharactersHero.Commands.CreateCharactersCommand;
using Domain.Entities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Persistence.Services;
using System.Net;
using System.Text.Json;

namespace WebAPI.Controllers.v1
{
    [ApiVersion("1.0")]
    public class WantedController : BaseApiController
    {
        private readonly TeamServices _teamServices;
        private readonly IHttpClientFactory _clientFactory;


        public WantedController(IHttpClientFactory clientFactory, TeamServices context)
        {
            _clientFactory = clientFactory;
            _teamServices = context;
        }

        

        [HttpGet]
        [Route("GeneratePDF")]
        public async Task<IActionResult> getPDFgenerator()
        {
            int indice = 0;
            byte[] imageData;

            var client = _clientFactory.CreateClient();
            string charAux = "";
            Random random = new Random();


            var url = _teamServices.getUrl("CharacterByid");
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                using var responseStream = await response.Content.ReadAsStreamAsync();
                var jsonOptions = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                };
                var characterInfo = await JsonSerializer.DeserializeAsync<MarvelCharacterDataWrapper>(responseStream, jsonOptions);
                var avengersCharacters = characterInfo.Data.Results;

                foreach (var character in avengersCharacters)
                {
                    url = _teamServices.getUrl("Stories"); ;
                    response = await client.GetAsync(url);

                    if (response.IsSuccessStatusCode)
                    {
                        using var responseStream2 = await response.Content.ReadAsStreamAsync();
                        jsonOptions = new JsonSerializerOptions
                        {
                            PropertyNameCaseInsensitive = true,
                        };
                        var storiesInfo = await JsonSerializer.DeserializeAsync<StoryDataWrapper>(responseStream2, jsonOptions);
                        var StoriesVillan = storiesInfo.data.results;

                        using (MemoryStream stream = new MemoryStream())
                        {


                            using (var doc = new iTextSharp.text.Document(PageSize.LETTER))
                            {
                                PdfWriter writer = PdfWriter.GetInstance(doc, stream);


                                doc.Open();


                                using (var webClient = new WebClient())
                                {
                                    imageData = webClient.DownloadData(character.thumbnail.path + "." + character.thumbnail.extension);

                                }

                                iTextSharp.text.Image cap = iTextSharp.text.Image.GetInstance(imageData);
                                doc.AddAuthor("Avengers");
                                doc.AddTitle("Capturados");
                                Paragraph title = new Paragraph("Villano: " + character.Name);
                                title.Alignment = Element.ALIGN_CENTER;
                                doc.Add(title);
                                cap.ScaleToFit(200f, 200f);
                                doc.Add(cap);
                                doc.Add(new Phrase("Descripcion del villano: \n " + character.Description));

                                foreach (var itemStories in StoriesVillan)
                                {
                                    if(!itemStories.description.Equals("")) { 
                                    doc.Add(new Phrase("Descripcion: " + itemStories.description));
                                    }

                                }

                            }

                            MemoryStream outputStream = new MemoryStream(stream.ToArray());

                            outputStream.Position = 0;
                            return new FileStreamResult(outputStream, "application/pdf");
                        }


                        return Ok(avengersCharacters);
                    }
                    else
                    {
                        return StatusCode((int)response.StatusCode);
                    }
                }
            }
            return Ok("");
        }



      
    }
}

