namespace RunningTracker.Application.RunningActivities
{
    public record RunningActivityResponse(
        Guid Id,
        string Location,
        DateTime StartTime,
        DateTime EndTime,
        double Distance,
        double Duration,
        double AveragePace);
}
