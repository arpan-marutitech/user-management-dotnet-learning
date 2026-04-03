using FastEndpoints;
using MediatR;
using UserManagement.Application.Commands;

namespace UserManagement.API.FastEndpoints.Users;

public class DeleteUserEndpoint : Endpoint<UserByIdRequest>
{
    private readonly IMediator _mediator;

    public DeleteUserEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Delete("fe/users/{id:guid}");
    }

    public override async Task HandleAsync(UserByIdRequest request, CancellationToken ct)
    {
        var deleted = await _mediator.Send(new DeleteUserCommand { Id = request.Id }, ct);
        if (!deleted)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(ct);
    }
}
