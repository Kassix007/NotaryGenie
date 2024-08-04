using global::NotaryGenie.Server.Services.Documents;
using Microsoft.AspNetCore.Mvc;
using NotaryGenie.Server.Data;
using NotaryGenie.Server.Models;
namespace NotaryGenie.Server.Controllers
{

    [Route("api/[controller]")]
        [ApiController]
        public class DocumentsController : ControllerBase
        {
            private readonly ApplicationDbContext _context;
            private readonly IDocumentService _documentService;
            private readonly ILogger<DocumentsController> _logger;

            public DocumentsController(ApplicationDbContext context, IDocumentService documentService, ILogger<DocumentsController> logger)
            {
                _context = context;
                _documentService = documentService;
                _logger = logger;
            }

            [HttpPost("upload")]
            public async Task<IActionResult> UploadDocument(IFormFile file, int clientId, string documentName, string description)
            {
                try
                {
                    if (file == null || file.Length == 0)
                        return BadRequest("File not selected");
                    var allowedExtensions = new[] { ".pdf", ".docx", ".txt", ".jpg", ".jpeg", ".png", ".gif" };
                    var fileExtension = Path.GetExtension(file.FileName).ToLower();

                    if (!allowedExtensions.Contains(fileExtension))
                    {
                        return BadRequest("Invalid file type.");
                    }

                    var client = await _context.Clients.FindAsync(clientId);
                    if (client == null)
                        return NotFound("Client not found");
                    //upload file to dir docStorage using service
                    var filePath = await _documentService.SaveFileAsync(file,(documentName+fileExtension));

                    var document = new Document(clientId, documentName, DateTime.UtcNow, filePath)
                    {
                        ClientID = clientId,
                        DocumentName = documentName,
                        UploadDate = DateTime.UtcNow,
                        FilePath = filePath
                    };
                    //add to db
                    _context.Documents.Add(document);
                    await _context.SaveChangesAsync();
                    //todo : implement extract keywords
                    var keywords = _documentService.ExtractKeywords(filePath);
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
                catch (Exception ex)
                {
                    _logger.LogError(ex, "An error occurred while uploading the document.");
                    return StatusCode(500, "An error occurred while uploading the document.");
                }
            }
        }
    

}
