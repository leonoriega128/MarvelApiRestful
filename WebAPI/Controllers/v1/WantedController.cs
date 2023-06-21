using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Persistence.Services;
using System.Net;

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

        [HttpGet("GeneratePDF")]
        public async Task<IActionResult> GeneratePDF()
        {
            int indice = 0;
            byte[] imageData;
            var resVillian = await _teamServices.SearchVillians();
            using (MemoryStream stream = new MemoryStream())
            {


                using (var doc = new iTextSharp.text.Document(PageSize.LETTER))
                {
                    PdfWriter writer = PdfWriter.GetInstance(doc, stream);


                    doc.Open();
                    foreach (var item in resVillian)
                    {

                        using (var webClient = new WebClient())
                        {
                            imageData = webClient.DownloadData(item.UrlImage);

                        }

                        iTextSharp.text.Image cap = iTextSharp.text.Image.GetInstance(imageData);
                        doc.AddAuthor("Avengers");
                        doc.AddTitle("Capturados");
                        Paragraph title = new Paragraph("Villano: " + item.Name);
                        title.Alignment = Element.ALIGN_CENTER;
                        doc.Add(title);
                        cap.ScaleToFit(200f, 200f);
                        doc.Add(cap);
                        doc.Add(new Phrase("Descripcion: " + item.Description));

                    }

                }

                MemoryStream outputStream = new MemoryStream(stream.ToArray());

                outputStream.Position = 0;
                return new FileStreamResult(outputStream, "application/pdf");
            }
            return Ok("");
        }




    }
}
