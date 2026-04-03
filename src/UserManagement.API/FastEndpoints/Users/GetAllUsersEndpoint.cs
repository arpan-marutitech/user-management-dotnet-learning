using FastEndpoints;
using MediatR;
using UserManagement.Application.DTOs;
using UserManagement.Application.Queries;

namespace UserManagement.API.FastEndpoints.Users;

public class GetAllUsersEndpoint : EndpointWithoutRequest<IEnumerable<UserResponseDto>>
{
    private readonly IMediator _mediator;

    public GetAllUsersEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("fe/users");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var users = await _mediator.Send(new GetAllUsersQuery(), ct);
        await SendOkAsync(users, ct);
    }
}
