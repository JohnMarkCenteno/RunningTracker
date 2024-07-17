namespace RunningTracker.Application.Users
{
    public record UserResponse(
        Guid Id,
        string Name,
        double Weight,
        double Height,
        DateTime BirthDate,
        int Age,
        double BMI);
}
