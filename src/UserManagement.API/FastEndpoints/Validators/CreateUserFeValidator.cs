using FastEndpoints;
using UserManagement.Application.DTOs;
using UserManagement.Application.Validators;

namespace UserManagement.API.FastEndpoints.Validators;

public class CreateUserFeValidator : Validator<CreateUserDto>
{
    public CreateUserFeValidator()
    {
        Include(new CreateUserValidator());
    }
}