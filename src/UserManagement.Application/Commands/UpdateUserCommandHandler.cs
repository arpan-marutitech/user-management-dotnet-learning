using MediatR;
using UserManagement.Application.DTOs;
using UserManagement.Application.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Commands;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UserResponseDto?>
{
    private readonly IUserRepository _userRepository;

    public UpdateUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponseDto?> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = new User
        {
            Id = request.Id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        };

        var updated = await _userRepository.UpdateAsync(user);
        if (!updated)
        {
            return null;
        }

        return new UserResponseDto
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        };
    }
}
