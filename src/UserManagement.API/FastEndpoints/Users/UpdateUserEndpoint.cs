using FastEndpoints;
using MediatR;
using UserManagement.Application.Commands;
using UserManagement.Application.DTOs;

namespace UserManagement.API.FastEndpoints.Users;

public class UpdateUserEndpoint : Endpoint<UpdateUserDto, UserResponseDto>
{
    private readonly IMediator _mediator;

    public UpdateUserEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Put("fe/users/{id:guid}");
    }

    public override async Task HandleAsync(UpdateUserDto request, CancellationToken ct)
    {
        var id = Route<Guid>("id");

        var command = new UpdateUserCommand
        {
            Id = id,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        };

        var response = await _mediator.Send(command, ct);
        if (response is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        await SendOkAsync(response, ct);
    }
}
