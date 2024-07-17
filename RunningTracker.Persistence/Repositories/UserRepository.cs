using Microsoft.EntityFrameworkCore;
using RunngTracker.Persistence;
using RunningTracker.Domain.Users;

namespace RunningTracker.Persistence.Repositories
{
    public class UserRepository(ApplicationDbContext context) : IUserRepository
    {
        public void Add(User user)
        {
            context.Users.Add(user);
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await context.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await context.Users
                .ToListAsync();
        }
    }
}
