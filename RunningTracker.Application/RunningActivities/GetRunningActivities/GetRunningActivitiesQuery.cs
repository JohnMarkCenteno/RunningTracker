using RunningTracker.Application.Abstractions.Messaging;

namespace RunningTracker.Application.RunningActivities.GetRunningActivities
{
    public sealed record GetRunningActivitiesQuery(Guid UserId) : IQuery<List<RunningActivityResponse>>;
}
