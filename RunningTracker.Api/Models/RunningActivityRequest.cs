namespace RunningTracker.Api.Models
{

    public record RunningActivityRequest(
        string Location,
        DateTime StartTime,
        DateTime EndTime,
        double Distance);
}
