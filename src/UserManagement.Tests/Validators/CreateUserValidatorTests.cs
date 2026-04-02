using FluentAssertions;
using UserManagement.Application.DTOs;
using UserManagement.Application.Validators;
using Xunit;

namespace UserManagement.Tests.Validators;

public class CreateUserValidatorTests
{
    private readonly CreateUserValidator _validator = new();

    [Fact]
    public void Validate_WithValidData_ShouldSucceed()
    {
        // Arrange
        var dto = new CreateUserDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john.doe@example.com"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    [InlineData("   ")]
    public void Validate_WithEmptyFirstName_ShouldFail(string firstName)
    {
        // Arrange
        var dto = new CreateUserDto
        {
            FirstName = firstName,
            LastName = "Doe",
            Email = "john.doe@example.com"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "FirstName");
    }

    [Fact]
    public void Validate_WithFirstNameTooShort_ShouldFail()
    {
        // Arrange
        var dto = new CreateUserDto
        {
            FirstName = "J",
            LastName = "Doe",
            Email = "john.doe@example.com"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_WithFirstNameTooLong_ShouldFail()
    {
        // Arrange
        var dto = new CreateUserDto
        {
            FirstName = new string('a', 51),
            LastName = "Doe",
            Email = "john.doe@example.com"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_WithInvalidEmail_ShouldFail()
    {
        // Arrange
        var dto = new CreateUserDto
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "not-an-email"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().ContainSingle(e => e.PropertyName == "Email");
    }
}
