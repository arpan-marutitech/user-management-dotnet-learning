using FastEndpoints;
using UserManagement.Application.DTOs;
using UserManagement.Application.Validators;

namespace UserManagement.API.FastEndpoints.Validators;

public class LoginFeValidator : Validator<LoginDto>
{
    public LoginFeValidator()
    {
        Include(new LoginDtoValidator());
    }
}