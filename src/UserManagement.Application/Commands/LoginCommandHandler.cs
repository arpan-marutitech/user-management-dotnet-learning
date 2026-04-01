using MediatR;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces;

namespace UserManagement.Application.Commands;

public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDto?>
{
    private readonly IAuthRepository _authRepository;
    private readonly ITokenService _tokenService;

    public LoginCommandHandler(IAuthRepository authRepository, ITokenService tokenService)
    {
        _authRepository = authRepository;
        _tokenService = tokenService;
    }

    public async Task<AuthResponseDto?> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var credential = await _authRepository.GetByUsernameAsync(request.Username);
        if (credential is null)
        {
            return null;
        }

        if (!BCrypt.Net.BCrypt.Verify(request.Password, credential.PasswordHash))
        {
            return null;
        }

        return new AuthResponseDto
        {
            Token = _tokenService.GenerateToken(credential),
            Username = credential.Username
        };
    }
}
