using FastEndpoints;
using MediatR;
using UserManagement.Application.Commands;
using UserManagement.Application.DTOs;

namespace UserManagement.API.FastEndpoints.Users;

public class CreateUserEndpoint : Endpoint<CreateUserDto, UserResponseDto>
{
    private readonly IMediator _mediator;

    public CreateUserEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("fe/users");
    }

    public override async Task HandleAsync(CreateUserDto request, CancellationToken ct)
    {
        var command = new CreateUserCommand
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        };

        var response = await _mediator.Send(command, ct);
        await SendCreatedAtAsync<GetUserByIdEndpoint>(new { id = response.Id }, response, cancellation: ct);
    }
}
