using Microsoft.AspNetCore.Mvc;
using NotaryGenie.Server.Services.OCRProcessors;
using System;
using System.Threading.Tasks;

namespace NotaryGenie.Server.Controllers.OCRProcessor
{
    [ApiController]
    [Route("api/[controller]")]
    public class IDcardController : ControllerBase
    {
        private readonly IIdCardFrontService _idCardFrontService;

        public IDcardController(IIdCardFrontService idCardFrontService)
        {
            _idCardFrontService = idCardFrontService;
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessDocument([FromForm] string filePath)
        {
            try
            {
                // Use the service to process the document
                var entities = await _idCardFrontService.ProcessDocumentAsync(filePath);

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
