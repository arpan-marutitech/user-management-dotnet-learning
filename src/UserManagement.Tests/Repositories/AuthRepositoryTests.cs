using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Persistence;
using UserManagement.Infrastructure.Repositories;
using Xunit;

namespace UserManagement.Tests.Repositories;

public class AuthRepositoryTests
{
    private ApplicationDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task AddAsync_WithValidCredential_ShouldAddToDatabase()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new AuthRepository(context);
        var credential = new AuthCredential
        {
            Id = Guid.NewGuid(),
            Username = "testuser",
            PasswordHash = "hashedpassword"
        };

        // Act
        await repository.AddAsync(credential);

        // Assert
        var saved = await context.AuthCredentials.FirstOrDefaultAsync(a => a.Username == "testuser");
        saved.Should().NotBeNull();
        saved!.PasswordHash.Should().Be("hashedpassword");
    }

    [Fact]
    public async Task UsernameExistsAsync_WithExistingUsername_ShouldReturnTrue()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new AuthRepository(context);

        context.AuthCredentials.Add(new AuthCredential
        {
            Id = Guid.NewGuid(),
            Username = "testuser",
            PasswordHash = "hashedpassword"
        });
        await context.SaveChangesAsync();

        // Act
        var result = await repository.UsernameExistsAsync("testuser");

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public async Task UsernameExistsAsync_WithNonExistentUsername_ShouldReturnFalse()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new AuthRepository(context);

        // Act
        var result = await repository.UsernameExistsAsync("nonexistent");

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public async Task GetByUsernameAsync_WithExistingUsername_ShouldReturnCredential()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new AuthRepository(context);

        context.AuthCredentials.Add(new AuthCredential
        {
            Id = Guid.NewGuid(),
            Username = "testuser",
            PasswordHash = "hashedpassword"
        });
        await context.SaveChangesAsync();

        // Act
        var result = await repository.GetByUsernameAsync("testuser");

        // Assert
        result.Should().NotBeNull();
        result!.Username.Should().Be("testuser");
        result.PasswordHash.Should().Be("hashedpassword");
    }

    [Fact]
    public async Task GetByUsernameAsync_WithNonExistentUsername_ShouldReturnNull()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new AuthRepository(context);

        // Act
        var result = await repository.GetByUsernameAsync("nonexistent");

        // Assert
        result.Should().BeNull();
    }
}
