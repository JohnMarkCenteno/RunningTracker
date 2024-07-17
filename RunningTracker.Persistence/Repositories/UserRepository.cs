using RunngTracker.Persistence;
using RunningTracker.Domain.Activities;
using RunningTracker.Domain.Users;

namespace RunningTracker.Persistence.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        public void Add(User user)
        {
            throw new NotImplementedException();
        }

        public void AddRunninActivity(Guid userId, RunningActivity runningActivity)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUserRunningActivitiesAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
