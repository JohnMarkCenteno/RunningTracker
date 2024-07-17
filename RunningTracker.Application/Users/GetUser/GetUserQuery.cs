using RunningTracker.Application.Abstractions.Messaging;

namespace RunningTracker.Application.Users.GetUser
{
    public sealed record GetUserQuery(Guid Id) : IQuery<UserResponse>;
}
