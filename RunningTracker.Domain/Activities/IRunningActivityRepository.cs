namespace RunningTracker.Domain.Activities
{
    public interface IRunningActivityRepository
    {
        Task<IEnumerable<RunningActivity>> GetUserRunningActivitiesAsync(Guid userId);
        Task<RunningActivity?> GetByIdAsync(Guid id);
        void Add(RunningActivity runningActivity);
    }
}
