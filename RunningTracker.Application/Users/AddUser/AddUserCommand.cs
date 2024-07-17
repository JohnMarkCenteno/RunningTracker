using RunningTracker.Application.Abstractions.Messaging;

namespace RunningTracker.Application.Users.AddUser
{
    public sealed record AddUserCommand(
        string Name,
        double Weight,
        double Height,
        DateTime BirthDate) : ICommand;
}
