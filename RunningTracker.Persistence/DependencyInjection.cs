using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RunngTracker.Persistence;
using RunningTracker.Application.Abstractions.Data;
using RunningTracker.Domain.Activities;
using RunningTracker.Domain.Users;
using RunningTracker.Persistence.Repositories;

namespace RunningTracker.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase(configuration.GetConnectionString("RunningActivityDatabase"));
            });

            services.AddScoped<IApplicationDbContext>(sp => sp.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRunningActivityRepository, RunningActivityRepository>();

            return services;
        }
    }
}
