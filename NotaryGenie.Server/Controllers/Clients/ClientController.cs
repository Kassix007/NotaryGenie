using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotaryGenie.Server.Data;
using NotaryGenie.Server.Dtos;
using NotaryGenie.Server.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotaryGenie.Server.Controllers.Clients
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ClientsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Clients/Notary/5
        [HttpGet("Notary/{notaryId}")]
        public async Task<ActionResult<IEnumerable<ClientDto>>> GetClientsByNotaryId(int notaryId)
        {
            var clients = await _context.Clients
                .Where(c => c.NotaryID == notaryId)
                .Select(c => new ClientDto
                {
                    ClientID = c.ClientID,
                    FirstName = c.FirstName,
                    Surname = c.Surname,
                    Email = c.Email,
                    Phone = c.Phone,
                    DateOfBirth = c.DateOfBirth,
                    Profession = c.Profession,
                    Documents = c.Documents.Select(d => new DocumentDto
                    {
                        DocumentID = d.DocumentID,
                        DocumentName = d.DocumentName,
                        UploadDate = d.UploadDate,
                        FilePath = d.FilePath
                    }).ToList(),
                    ClientDeeds = c.ClientDeeds.Select(cd => new ClientDeedDto
                    {
                        DeedID = cd.DeedID,                       
                    }).ToList()
                })
                .ToListAsync();

            if (clients == null || !clients.Any())
            {
                return NotFound();
            }

            return Ok(clients);
        }
    }
}
