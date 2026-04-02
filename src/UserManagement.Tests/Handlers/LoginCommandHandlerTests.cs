using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UserManagement.Application.Commands;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Persistence;
using UserManagement.Infrastructure.Repositories;
using UserManagement.Infrastructure.Services;
using Xunit;
using BCrypt.Net;

namespace UserManagement.Tests.Handlers;

public class LoginCommandHandlerTests
{
    private ApplicationDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    private IConfiguration CreateConfigurationWithJwtSettings()
    {
        var inMemorySettings = new Dictionary<string, string?>
        {
            { "Jwt:Key", "SuperSecretKey_ChangeThisInProduction_MinLength32Chars!" },
            { "Jwt:Issuer", "UserManagementAPI" },
            { "Jwt:Audience", "UserManagementClient" }
        };

        return new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
    }

    [Fact]
    public async Task Handle_WithValidCredentials_ShouldReturnAuthResponseWithToken()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var configuration = CreateConfigurationWithJwtSettings();

        var authRepository = new AuthRepository(context);
        var tokenService = new TokenService(configuration);
        var handler = new LoginCommandHandler(authRepository, tokenService);

        const string username = "testuser";
        const string password = "SecurePass@123";
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

        var credential = new AuthCredential
        {
            Id = Guid.NewGuid(),
            Username = username,
            PasswordHash = passwordHash
        };

        await authRepository.AddAsync(credential);

        var command = new LoginCommand
        {
            Username = username,
            Password = password
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result!.Token.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Handle_WithInvalidPassword_ShouldReturnNull()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var configuration = CreateConfigurationWithJwtSettings();

        var authRepository = new AuthRepository(context);
        var tokenService = new TokenService(configuration);
        var handler = new LoginCommandHandler(authRepository, tokenService);

        const string username = "testuser";
        const string correctPassword = "SecurePass@123";
        const string wrongPassword = "WrongPassword@123";
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(correctPassword);

        var credential = new AuthCredential
        {
            Id = Guid.NewGuid(),
            Username = username,
            PasswordHash = passwordHash
        };

        await authRepository.AddAsync(credential);

        var command = new LoginCommand
        {
            Username = username,
            Password = wrongPassword
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task Handle_WithNonExistentUsername_ShouldReturnNull()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var configuration = CreateConfigurationWithJwtSettings();

        var authRepository = new AuthRepository(context);
        var tokenService = new TokenService(configuration);
        var handler = new LoginCommandHandler(authRepository, tokenService);

        var command = new LoginCommand
        {
            Username = "nonexistent",
            Password = "SomePassword@123"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeNull();
    }
}
