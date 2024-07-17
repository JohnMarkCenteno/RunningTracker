

using RunningTracker.Infrastructure.Shared;

namespace RunningTracker.Domain.Activities
{
    public static class RunninActivityErrors
    {
        public static Error NotFound(Guid id) => new(
            "RunningActivities.NotFound", $"Running Activity with the Id = '{id}' was not found.");

        public static Error NotFoundForUser(Guid userId) => new(
            "RunningActivities.NotFound", $"Running Activities for User with the Id = '{userId}' was not found.");
    }
}
