using FastEndpoints;
using UserManagement.Application.DTOs;
using UserManagement.Application.Validators;

namespace UserManagement.API.FastEndpoints.Validators;

public class RegisterFeValidator : Validator<RegisterDto>
{
    public RegisterFeValidator()
    {
        Include(new RegisterDtoValidator());
    }
}