using RunningTracker.Domain.Activities;

namespace RunningTracker.Domain.Users
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<IEnumerable<User>> GetUsersAsync();
        void Add(User user);
    }
}
