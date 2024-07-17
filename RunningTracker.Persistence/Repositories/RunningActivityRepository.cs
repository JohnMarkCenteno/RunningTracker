using RunngTracker.Persistence;
using RunningTracker.Domain.Activities;

namespace RunningTracker.Persistence.Repositories
{
    public class RunningActivityRepository(ApplicationDbContext context) : IRunningActivityRepository
    {
        public void Add(RunningActivity runningActivity)
        {
            throw new NotImplementedException();
        }

        public Task<RunningActivity> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<RunningActivity>> GetUserRunningActivitiesAsync(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
