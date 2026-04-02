using FluentAssertions;
using UserManagement.Infrastructure.Resilience;
using Xunit;

namespace UserManagement.Tests.Resilience;

public class ResiliencePipelineTests
{
    [Fact]
    public async Task DatabaseRead_WithSuccessfulExecution_ShouldReturnResult()
    {
        // Arrange
        const string expected = "success";

        // Act
        var result = await ResiliencePipelines.DatabaseRead.ExecuteAsync(
            async _ => await Task.FromResult(expected),
            CancellationToken.None);

        // Assert
        result.Should().Be(expected);
    }

    [Fact]
    public async Task DatabaseRead_RetriesOnTransientException_SucceedsOnThirdAttempt()
    {
        // Arrange
        int callCount = 0;

        // Act
        var result = await ResiliencePipelines.DatabaseRead.ExecuteAsync(
            async _ =>
            {
                callCount++;
                if (callCount < 3)
                    throw new InvalidOperationException("Transient error");
                return await Task.FromResult("success");
            },
            CancellationToken.None);

        // Assert
        result.Should().Be("success");
        callCount.Should().Be(3); // Failed twice, succeeded on 3rd
    }

    [Fact]
    public async Task DatabaseRead_WithCancellation_ShouldThrowOperationCanceledException()
    {
        // Arrange
        using var cts = new CancellationTokenSource();
        cts.CancelAfter(500); // Cancel after 500ms

        // Act & Assert
        var act = async () =>
        {
            await ResiliencePipelines.DatabaseRead.ExecuteAsync(
                async cancellationToken =>
                {
                    await Task.Delay(5000, cancellationToken); // Will be canceled
                    return "never";
                },
                cts.Token);
        };

        await act.Should().ThrowAsync<OperationCanceledException>();
}
}
