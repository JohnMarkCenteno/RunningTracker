using Microsoft.EntityFrameworkCore;
using RunningTracker.Application.Abstractions.Data;
using RunningTracker.Domain.Activities;
using RunningTracker.Domain.Users;
using RunningTracker.Persistence.Configurations;

namespace RunngTracker.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext, IUnitOfWork
    {
        public DbSet<User> Users { get; set; }
        public DbSet<RunningActivity> RunningActivities { get; set; }


        public ApplicationDbContext() : base(new DbContextOptions<ApplicationDbContext>()) { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("RunningActivityDatabase");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new RunningActivityConfiguration());
        }
    }
}
