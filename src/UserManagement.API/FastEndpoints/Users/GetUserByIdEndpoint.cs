using FastEndpoints;
using MediatR;
using UserManagement.Application.Queries;

namespace UserManagement.API.FastEndpoints.Users;

public class GetUserByIdEndpoint : Endpoint<UserByIdRequest>
{
    private readonly IMediator _mediator;

    public GetUserByIdEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Get("fe/users/{id:guid}");
    }

    public override async Task HandleAsync(UserByIdRequest request, CancellationToken ct)
    {
        var user = await _mediator.Send(new GetUserByIdQuery { Id = request.Id }, ct);
        if (user is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(user, ct);
    }
}
