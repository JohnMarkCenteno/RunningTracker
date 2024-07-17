using RunningTracker.Infrastructure.Shared;

namespace RunningTracker.Domain.Users
{
    public static class UserErrors
    {
        public static Error NotFound(Guid id) => new(
            "Users.NotFound", $"User with the Id = '{id}' was not found.");

        public static Error NotFound() => new(
            "Users.NotFound", $"No users was found.");
    }
}
