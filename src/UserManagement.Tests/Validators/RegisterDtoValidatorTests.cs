using FluentAssertions;
using UserManagement.Application.DTOs;
using UserManagement.Application.Validators;
using Xunit;

namespace UserManagement.Tests.Validators;

public class RegisterDtoValidatorTests
{
    private readonly RegisterDtoValidator _validator = new();

    [Fact]
    public void Validate_WithValidData_ShouldSucceed()
    {
        // Arrange
        var dto = new RegisterDto
        {
            Username = "JohnDoe123",
            Password = "SecurePass@123"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Theory]
    [InlineData("")]
    public void Validate_WithEmptyUsername_ShouldFail(string username)
    {
        // Arrange
        var dto = new RegisterDto
        {
            Username = username,
            Password = "SecurePass@123"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_WithUsernameTooShort_ShouldFail()
    {
        // Arrange
        var dto = new RegisterDto
        {
            Username = "ab",
            Password = "SecurePass@123"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_WithInvalidUsernameCharacters_ShouldFail()
    {
        // Arrange
        var dto = new RegisterDto
        {
            Username = "John@Doe!",
            Password = "SecurePass@123"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
        result.Errors.Should().Contain(e => e.PropertyName == "Username");
    }

    [Fact]
    public void Validate_WithPasswordTooShort_ShouldFail()
    {
        // Arrange
        var dto = new RegisterDto
        {
            Username = "JohnDoe",
            Password = "Pass@1"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_WithPasswordMissingUppercase_ShouldFail()
    {
        // Arrange
        var dto = new RegisterDto
        {
            Username = "JohnDoe",
            Password = "securepass@123"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public void Validate_WithPasswordMissingSpecialCharacter_ShouldFail()
    {
        // Arrange
        var dto = new RegisterDto
        {
            Username = "JohnDoe",
            Password = "SecurePass123"
        };

        // Act
        var result = _validator.Validate(dto);

        // Assert
        result.IsValid.Should().BeFalse();
    }
}
