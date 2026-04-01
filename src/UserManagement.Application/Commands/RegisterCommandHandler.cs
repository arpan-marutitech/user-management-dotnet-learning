using MediatR;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Commands;

public class RegisterCommandHandler : IRequestHandler<RegisterCommand, AuthResponseDto>
{
    private readonly IAuthRepository _authRepository;
    private readonly ITokenService _tokenService;

    public RegisterCommandHandler(IAuthRepository authRepository, ITokenService tokenService)
    {
        _authRepository = authRepository;
        _tokenService = tokenService;
    }

    public async Task<AuthResponseDto> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        if (await _authRepository.UsernameExistsAsync(request.Username))
        {
            throw new InvalidOperationException("Username already exists.");
        }

        var credential = new AuthCredential
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        await _authRepository.AddAsync(credential);

        return new AuthResponseDto
        {
            Token = _tokenService.GenerateToken(credential),
            Username = credential.Username
        };
    }
}
