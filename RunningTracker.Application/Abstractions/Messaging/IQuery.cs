using MediatR;
using RunningTracker.Infrastructure.Shared;

namespace RunningTracker.Application.Abstractions.Messaging
{
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
