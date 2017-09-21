using System;
using System.Collections.Generic;
using System.Net.Http;
using Polly;

namespace ResilientHttpClientApp.Std.ResilienceHttp
{
    public class ResilientHttpClientFactory
    {
        public ResilientHttpClient CreateResilientHttpClient()
        {
            return new ResilientHttpClient(origin => CreatePolicies());
        }

        private IEnumerable<Policy> CreatePolicies()
        {
            var waitAndRetryPolicy = Policy.Handle<HttpRequestException>()
                // Policy 1: wait and retry policy (bypasses internet connectivity issues)
                .WaitAndRetryAsync(
                    // number of retries
                    6,
                    // exponential backofff
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    // on retry
                    (exception, timeSpan, retryCount, context) =>
                    {
                        //var msg = $"Retry {retryCount} implemented with Polly's RetryPolicy " +
                        //    $"of {context.PolicyKey} " +
                        //    $"at {context.ExecutionKey}, " +
                        //    $"due to: {exception}.";
                    });

            // Policy 2: circuit breaker (not overload the server)
            var circuitBreakerPolicy = Policy.Handle<HttpRequestException>()
                .CircuitBreakerAsync(
                    // number of exceptions before breaking circuit
                    5,
                    // time circuit opened before retry
                    TimeSpan.FromMinutes(1),
                    (exception, duration) =>
                    {
                        // on circuit opened
                        //_logger.LogTrace("Circuit breaker opened");
                    },
                    () =>
                    {
                        // on circuit closed
                        //_logger.LogTrace("Circuit breaker reset");
                    });

            return new List<Policy>
            {
                waitAndRetryPolicy,
                circuitBreakerPolicy
            };
        }
    }
}