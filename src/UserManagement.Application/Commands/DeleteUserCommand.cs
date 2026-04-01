using MediatR;

namespace UserManagement.Application.Commands;

public class DeleteUserCommand : IRequest<bool>
{
    public Guid Id { get; set; }
}
