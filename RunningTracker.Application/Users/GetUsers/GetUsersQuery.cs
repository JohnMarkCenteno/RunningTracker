using RunningTracker.Application.Abstractions.Messaging;

namespace RunningTracker.Application.Users.GetUsers
{
    public sealed record GetUsersQuery() : IQuery<List<UserResponse>>;
}
