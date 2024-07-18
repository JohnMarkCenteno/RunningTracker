using MediatR;
using Microsoft.Extensions.Logging;
using RunningTracker.Infrastructure.Shared;

namespace RunningTracker.Application.Behaviors
{
    public class LoggingPipelineBehavior<TRequest, TResponse>(ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : Result
    {
        private readonly ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger = logger;

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            this.logger.LogInformation(
                "Starting request {@RequestName}, {@DateTimeUtc}",
                typeof(TRequest).Name,
                DateTime.UtcNow);

            var result = await next();

            if (result.IsFailure)
            {
                this.logger.LogError(
                "Request failure {@RequestName}, {@Error}, {@DateTimeUtc}",
                typeof(TRequest).Name,
                result.Error.Code,
                DateTime.UtcNow);
            }

            this.logger.LogInformation(
                "Completed request {@RequestName}, {@DateTimeUtc}",
                typeof(TRequest).Name,
                DateTime.UtcNow);

            return result;
        }
    }
}
