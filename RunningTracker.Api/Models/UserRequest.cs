namespace RunningTracker.Api.Models
{
    public record UserRequest(
        string Name,
        double Weight,
        double Height,
        DateTime BirthDate);
}
