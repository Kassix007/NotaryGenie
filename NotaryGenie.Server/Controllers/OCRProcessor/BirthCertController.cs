using Microsoft.AspNetCore.Mvc;
using NotaryGenie.Server.Services.OCRProcessors;

namespace NotaryGenie.Server.Controllers.OCRProcessor
{
    [ApiController]
    [Route("api/[controller]")]
    public class BirthCertController : ControllerBase
    {
        private readonly IBirthCertService _birthCertService;

        public BirthCertController(IBirthCertService birthCertService)
        {
            _birthCertService = birthCertService;
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessDocument([FromForm] string filePath)
        {
            try
            {
                var entities = await _birthCertService.ProcessDocumentAsync(filePath);
                return Ok(new { Entities = entities });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}