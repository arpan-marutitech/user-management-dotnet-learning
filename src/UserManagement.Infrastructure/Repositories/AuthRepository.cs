using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Persistence;

namespace UserManagement.Infrastructure.Repositories;

public class AuthRepository : IAuthRepository
{
    private readonly ApplicationDbContext _context;

    public AuthRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> UsernameExistsAsync(string username)
    {
        return await _context.AuthCredentials.AnyAsync(x => x.Username == username);
    }

    public async Task AddAsync(AuthCredential credential)
    {
        _context.AuthCredentials.Add(credential);
        await _context.SaveChangesAsync();
    }

    public async Task<AuthCredential?> GetByUsernameAsync(string username)
    {
        return await _context.AuthCredentials.FirstOrDefaultAsync(x => x.Username == username);
    }
}
