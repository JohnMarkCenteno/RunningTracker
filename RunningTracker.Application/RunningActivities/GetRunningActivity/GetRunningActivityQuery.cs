using RunningTracker.Application.Abstractions.Messaging;

namespace RunningTracker.Application.RunningActivities.GetRunningActivity
{
    public sealed record GetRunningActivityQuery(Guid Id) : IQuery<RunningActivityResponse>;
}
