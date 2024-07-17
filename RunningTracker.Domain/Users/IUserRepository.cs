using RunningTracker.Domain.Activities;

namespace RunningTracker.Domain.Users
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetUserRunningActivitiesAsync(Guid userId);
        Task<User> GetByIdAsync(Guid id);
        void Add(User user);
        void AddRunninActivity(Guid userId, RunningActivity runningActivity);
    }
}
