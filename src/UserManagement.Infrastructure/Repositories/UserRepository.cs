using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Persistence;
using UserManagement.Infrastructure.Resilience;

namespace UserManagement.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User> AddAsync(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await ResiliencePipelines.DatabaseRead.ExecuteAsync(
            async cancellationToken => await _context.Users
                .AsNoTracking()
                .Where(x => !string.IsNullOrWhiteSpace(x.Email))
                .OrderBy(x => x.FirstName)
                .ThenBy(x => x.LastName)
                .ToListAsync(cancellationToken),
            CancellationToken.None);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        return await ResiliencePipelines.DatabaseRead.ExecuteAsync(
            async cancellationToken => await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken),
            CancellationToken.None);
    }

    public async Task<bool> UpdateAsync(User user)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
        if (existingUser is null)
        {
            return false;
        }

        existingUser.FirstName = user.FirstName;
        existingUser.LastName = user.LastName;
        existingUser.Email = user.Email;

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var existingUser = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        if (existingUser is null)
        {
            return false;
        }

        _context.Users.Remove(existingUser);
        await _context.SaveChangesAsync();
        return true;
    }
}
