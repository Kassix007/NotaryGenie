using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotaryGenie.Server.Data;
using System.Threading.Tasks;
namespace NotaryGenie.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TestControllerDB(ApplicationDbContext context) : ControllerBase
{
    private readonly ApplicationDbContext _context = context;

    [HttpGet("test-db-connection")]
    public async Task<IActionResult> TestDbConnection()
    {
        try
        {
            var notaries = await _context.Notaries.FirstOrDefaultAsync();
            return Ok(new { success = true, message = "Database connection is successful." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { success = false, message = "Database connection failed.", error = ex.Message });
        }
    }
}
