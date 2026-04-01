using MediatR;
using UserManagement.Application.DTOs;

namespace UserManagement.Application.Queries;

public class GetUserByIdQuery : IRequest<UserResponseDto?>
{
    public Guid Id { get; set; }
}
