using FastEndpoints;
using MediatR;
using UserManagement.Application.Commands;
using UserManagement.Application.DTOs;

namespace UserManagement.API.FastEndpoints.Auth;

public class LoginEndpoint : Endpoint<LoginDto, AuthResponseDto>
{
    private readonly IMediator _mediator;

    public LoginEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("fe/auth/login");
        AllowAnonymous();
    }

    public override async Task HandleAsync(LoginDto request, CancellationToken ct)
    {
        var command = new LoginCommand
        {
            Username = request.Username,
            Password = request.Password
        };

        var response = await _mediator.Send(command, ct);
        if (response is null)
        {
            await SendUnauthorizedAsync(ct);
            return;
        }

        await SendOkAsync(response, ct);
    }
}
