using Microsoft.AspNetCore.Mvc;
using NotaryGenie.Server.Services.OCRProcessors;

namespace NotaryGenie.Server.Controllers.OCRProcessor
{
    [ApiController]
    [Route("api/[controller]")]
    public class CWAController : ControllerBase
    {
        private readonly ICWAService _cwaService;

        public CWAController(ICWAService cwaService)
        {
            _cwaService = cwaService;
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessDocument([FromForm] string filePath)
        {
            try
            {
                var entities = await _cwaService.ProcessDocumentAsync(filePath);

                return Ok(new
                {
                    Entities = entities
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
