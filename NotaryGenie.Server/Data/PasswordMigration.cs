using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NotaryGenie.Server.Data;
using NotaryGenie.Server.Models;
using System;
using System.Threading.Tasks;

public class PasswordMigrationService
{
    private readonly ApplicationDbContext _context;
    private readonly PasswordHasher<Notary> _passwordHasher;

    public PasswordMigrationService(ApplicationDbContext context)
    {
        _context = context;
        _passwordHasher = new PasswordHasher<Notary>();
    }

    public async Task MigratePasswordsAsync()
    {
        var notaries = await _context.Notaries.ToListAsync();

        foreach (var notary in notaries)
        {
            // Check if the password is plain text (i.e., not Base64 encoded)
            if (!IsBase64String(notary.Password))
            {
                // Hash the plain text password
                notary.Password = _passwordHasher.HashPassword(notary, notary.Password);
            }
        }

        await _context.SaveChangesAsync();
    }

    private bool IsBase64String(string base64String)
    {
        Span<byte> buffer = new Span<byte>(new byte[base64String.Length]);
        return Convert.TryFromBase64String(base64String, buffer, out _);
    }
}
