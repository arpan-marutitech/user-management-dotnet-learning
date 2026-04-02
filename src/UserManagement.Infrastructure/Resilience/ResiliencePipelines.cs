using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using Polly.Timeout;

namespace UserManagement.Infrastructure.Resilience;

internal static class ResiliencePipelines
{
    internal static readonly ResiliencePipeline DatabaseRead = new ResiliencePipelineBuilder()
        .AddRetry(new RetryStrategyOptions
        {
            MaxRetryAttempts = 3,
            Delay = TimeSpan.FromSeconds(2),
            BackoffType = DelayBackoffType.Exponential,
            UseJitter = true
        })
        .AddCircuitBreaker(new CircuitBreakerStrategyOptions
        {
            FailureRatio = 0.5,
            SamplingDuration = TimeSpan.FromSeconds(20),
            MinimumThroughput = 4,
            BreakDuration = TimeSpan.FromSeconds(10)
        })
        .AddTimeout(TimeSpan.FromSeconds(10))
        .Build();
}