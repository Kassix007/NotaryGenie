using Microsoft.AspNetCore.Mvc;
using NotaryGenie.Server.Data;
using NotaryGenie.Server.Services.Documents;

namespace NotaryGenie.Server.Controllers.TempUpload
{
    [Route("api/[controller]")]
    [ApiController]
    public class TempUploadDocumentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IDocumentService _documentService;
        private readonly ILogger<TempUploadDocumentsController> _logger;

        public TempUploadDocumentsController(ApplicationDbContext context, IDocumentService documentService, ILogger<TempUploadDocumentsController> logger)
        {
            _context = context;
            _documentService = documentService;
            _logger = logger;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadDocument([FromForm] IFormFile file, [FromForm] string documentName)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest("File not selected.");
                }

                var allowedExtensions = new[] { ".pdf", ".docx", ".txt", ".jpg", ".jpeg", ".png", ".gif" };
                var fileExtension = Path.GetExtension(file.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    return BadRequest("Invalid file type.");
                }

                // Define temporary storage path
                var storagePath = Path.Combine(Directory.GetCurrentDirectory(), "TempDocumentStorage");
                if (!Directory.Exists(storagePath))
                {
                    Directory.CreateDirectory(storagePath);
                }

                // Define the full file path
                var filePath = Path.Combine(storagePath, $"{documentName}{fileExtension}");

                // Save file
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                _logger.LogInformation($"File {documentName} uploaded successfully to temporary storage.");

                return Ok(new { FilePath = filePath});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while uploading the document.");
                return StatusCode(500, "An error occurred while uploading the document.");
            }
        }
    }
}
