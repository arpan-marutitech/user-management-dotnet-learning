using FastEndpoints;
using UserManagement.Application.DTOs;
using UserManagement.Application.Validators;

namespace UserManagement.API.FastEndpoints.Validators;

public class UpdateUserFeValidator : Validator<UpdateUserDto>
{
    public UpdateUserFeValidator()
    {
        Include(new UpdateUserValidator());
    }
}