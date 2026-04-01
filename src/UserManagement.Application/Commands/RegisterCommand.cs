using MediatR;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Commands;

public class RegisterCommand : IRequest<AuthResponseDto>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
