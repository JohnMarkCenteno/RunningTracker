using Microsoft.EntityFrameworkCore;
using RunningTracker.Domain.Activities;
using RunningTracker.Domain.Users;

namespace RunningTracker.Application.Abstractions.Data
{
    public interface IApplicationDbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RunningActivity> RunningActivities { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
