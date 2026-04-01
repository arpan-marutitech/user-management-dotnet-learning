using UserManagement.Domain.Entities;

namespace UserManagement.Application.Interfaces;

public interface IAuthRepository
{
    Task<bool> UsernameExistsAsync(string username);
    Task AddAsync(AuthCredential credential);
    Task<AuthCredential?> GetByUsernameAsync(string username);
}
