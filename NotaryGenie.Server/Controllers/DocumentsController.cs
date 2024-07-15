using Microsoft.AspNetCore.Mvc;
using NotaryGenie.Server.Data;
using NotaryGenie.Server.Models;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NotaryGenie.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public DocumentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadDocument(IFormFile file, int clientId, string documentName, string description)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File not selected");

            var client = await _context.Clients.FindAsync(clientId);
            if (client == null)
                return NotFound("Client not found");

            var storagePath = Path.Combine(Directory.GetCurrentDirectory(), "DocumentStorage");
            if (!Directory.Exists(storagePath))
            {
                Directory.CreateDirectory(storagePath);
            }

            var filePath = Path.Combine(storagePath, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var document = new Document(clientId, documentName, DateTime.UtcNow, filePath)
            {
                Client = client
            };

            _context.Documents.Add(document);
            await _context.SaveChangesAsync();

            var keywords = ExtractKeywords(filePath);
            foreach (var keyword in keywords)
            {
                var documentIndex = new DocumentIndex
                {
                    DocumentID = document.DocumentID,
                    Keyword = keyword,
                    Location = filePath
                };
                _context.DocumentIndex.Add(documentIndex);
            }
            await _context.SaveChangesAsync();

            return Ok(new { documentId = document.DocumentID });
        }

        private static string[] ExtractKeywords(string filePath)
        {         
            return new string[] { "keyword1", "keyword2" };
        }
    }
}
