using RunningTracker.Application.Abstractions.Messaging;

namespace RunningTracker.Application.RunningActivities.AddRunningActivity
{
    public sealed record AddRunningActivityCommand(
        Guid UserId,
        string Location,
        DateTime StartTime,
        DateTime EndTime,
        double Distance) : ICommand;
}
