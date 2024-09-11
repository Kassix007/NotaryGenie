using Microsoft.AspNetCore.Mvc;
using NotaryGenie.Server.Services.OCRProcessors;
using System;
using System.Threading.Tasks;

namespace NotaryGenie.Server.Controllers.OCRProcessor
{
    [ApiController]
    [Route("api/[controller]")]
    public class IDCardBackController : ControllerBase
    {
        private readonly IIdCardBackService _idCardBackService;

        public IDCardBackController(IIdCardBackService idCardBackService)
        {
            _idCardBackService = idCardBackService;
        }

        [HttpPost("process")]
        public async Task<IActionResult> ProcessDocument([FromForm] string filePath)
        {
            try
            {
                var entities = await _idCardBackService.ProcessDocumentAsync(filePath);

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
