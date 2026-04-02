using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using UserManagement.Application.Commands;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Persistence;
using UserManagement.Infrastructure.Repositories;
using Xunit;

namespace UserManagement.Tests.Handlers;

public class CreateUserCommandHandlerTests
{
    private ApplicationDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldCreateUser()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new UserRepository(context);
        var handler = new CreateUserCommandHandler(repository);

        var command = new CreateUserCommand
        {
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.FirstName.Should().Be("John");
        result.Email.Should().Be("john@example.com");

        var savedUser = await context.Users.FirstOrDefaultAsync(u => u.Email == "john@example.com");
        savedUser.Should().NotBeNull();
    }

    [Fact]
    public async Task Handle_WithValidCommand_ShouldReturnUserResponseDto()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new UserRepository(context);
        var handler = new CreateUserCommandHandler(repository);

        var command = new CreateUserCommand
        {
            FirstName = "Jane",
            LastName = "Smith",
            Email = "jane@example.com"
        };

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Id.Should().NotBeEmpty();
        result.FirstName.Should().Be("Jane");
        result.LastName.Should().Be("Smith");
        result.Email.Should().Be("jane@example.com");
    }
}
