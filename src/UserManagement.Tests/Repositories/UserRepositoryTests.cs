using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using UserManagement.Domain.Entities;
using UserManagement.Infrastructure.Persistence;
using UserManagement.Infrastructure.Repositories;
using Xunit;

namespace UserManagement.Tests.Repositories;

public class UserRepositoryTests
{
    private ApplicationDbContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new ApplicationDbContext(options);
    }

    [Fact]
    public async Task AddAsync_WithValidUser_ShouldAddToDatabase()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new UserRepository(context);
        var user = new User
        {
            Id = Guid.NewGuid(),
            FirstName = "John",
            LastName = "Doe",
            Email = "john@example.com"
        };

        // Act
        var result = await repository.AddAsync(user);

        // Assert
        result.Should().NotBeNull();
        result.Email.Should().Be("john@example.com");

        var savedUser = await context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);
        savedUser.Should().NotBeNull();
        savedUser!.FirstName.Should().Be("John");
    }

    [Fact]
    public async Task GetAllAsync_WithMultipleUsers_ShouldReturnOrderedList()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new UserRepository(context);

        var user1 = new User { Id = Guid.NewGuid(), FirstName = "Zoe", LastName = "Smith", Email = "zoe@example.com" };
        var user2 = new User { Id = Guid.NewGuid(), FirstName = "Alice", LastName = "Johnson", Email = "alice@example.com" };
        var user3 = new User { Id = Guid.NewGuid(), FirstName = "Bob", LastName = "Brown", Email = "bob@example.com" };

        await repository.AddAsync(user1);
        await repository.AddAsync(user2);
        await repository.AddAsync(user3);

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        result.Should().HaveCount(3);
        var list = result.ToList();
        list[0].FirstName.Should().Be("Alice"); // Ordered by FirstName
        list[1].FirstName.Should().Be("Bob");
        list[2].FirstName.Should().Be("Zoe");
    }

    [Fact]
    public async Task GetByIdAsync_WithValidId_ShouldReturnUser()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new UserRepository(context);
        var userId = Guid.NewGuid();
        var user = new User { Id = userId, FirstName = "John", LastName = "Doe", Email = "john@example.com" };

        await repository.AddAsync(user);

        // Act
        var result = await repository.GetByIdAsync(userId);

        // Assert
        result.Should().NotBeNull();
        result!.FirstName.Should().Be("John");
    }

    [Fact]
    public async Task GetByIdAsync_WithInvalidId_ShouldReturnNull()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new UserRepository(context);

        // Act
        var result = await repository.GetByIdAsync(Guid.NewGuid());

        // Assert
        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateAsync_WithValidUser_ShouldUpdateDatabase()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new UserRepository(context);
        var userId = Guid.NewGuid();
        var user = new User { Id = userId, FirstName = "John", LastName = "Doe", Email = "john@example.com" };

        await repository.AddAsync(user);

        // Act
        user.FirstName = "Jane";
        var result = await repository.UpdateAsync(user);

        // Assert
        result.Should().BeTrue();

        var updatedUser = await repository.GetByIdAsync(userId);
        updatedUser!.FirstName.Should().Be("Jane");
    }

    [Fact]
    public async Task DeleteAsync_WithValidId_ShouldRemoveFromDatabase()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new UserRepository(context);
        var userId = Guid.NewGuid();
        var user = new User { Id = userId, FirstName = "John", LastName = "Doe", Email = "john@example.com" };

        await repository.AddAsync(user);

        // Act
        var result = await repository.DeleteAsync(userId);

        // Assert
        result.Should().BeTrue();

        var deletedUser = await repository.GetByIdAsync(userId);
        deletedUser.Should().BeNull();
    }

    [Fact]
    public async Task DeleteAsync_WithInvalidId_ShouldReturnFalse()
    {
        // Arrange
        using var context = CreateInMemoryContext();
        var repository = new UserRepository(context);

        // Act
        var result = await repository.DeleteAsync(Guid.NewGuid());

        // Assert
        result.Should().BeFalse();
    }
}
