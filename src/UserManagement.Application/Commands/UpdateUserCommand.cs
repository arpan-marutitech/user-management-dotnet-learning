using MediatR;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Commands;

public class UpdateUserCommand : IRequest<UserResponseDto?>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}
