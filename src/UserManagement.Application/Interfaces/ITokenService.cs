using UserManagement.Domain.Entities;

namespace UserManagement.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(AuthCredential credential);
}
