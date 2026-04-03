using FastEndpoints;
using MediatR;
using UserManagement.Application.Commands;
using UserManagement.Application.DTOs;

namespace UserManagement.API.FastEndpoints.Auth;

public class RegisterEndpoint : Endpoint<RegisterDto, AuthResponseDto>
{
    private readonly IMediator _mediator;

    public RegisterEndpoint(IMediator mediator)
    {
        _mediator = mediator;
    }

    public override void Configure()
    {
        Post("fe/auth/register");
        AllowAnonymous();
    }

    public override async Task HandleAsync(RegisterDto request, CancellationToken ct)
    {
        try
        {
            var command = new RegisterCommand
            {
                Username = request.Username,
                Password = request.Password
            };

            var response = await _mediator.Send(command, ct);
            await SendOkAsync(response, ct);
        }
        catch (InvalidOperationException ex)
        {
            AddError(request => request.Username, ex.Message);
            await SendErrorsAsync(400, ct);
        }
    }
}
