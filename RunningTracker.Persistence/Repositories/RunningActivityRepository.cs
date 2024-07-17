using Microsoft.EntityFrameworkCore;
using RunngTracker.Persistence;
using RunningTracker.Domain.Activities;

namespace RunningTracker.Persistence.Repositories
{
    public class RunningActivityRepository(ApplicationDbContext context) : IRunningActivityRepository
    {
        public void Add(RunningActivity runningActivity)
        {
            context.RunningActivities.Add(runningActivity);
        }

        public async Task<RunningActivity?> GetByIdAsync(Guid id)
        {
            return await context.RunningActivities
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<RunningActivity>> GetUserRunningActivitiesAsync(Guid userId)
        {
            return await context.RunningActivities
                .Include(e => e.User)
                .Where(u => u.UserId == userId)
                .ToListAsync();
        }
    }
}
