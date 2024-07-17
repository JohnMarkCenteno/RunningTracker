using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RunngTracker.Persistence;
using RunningTracker.Application.Abstractions.Data;

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

            // todo services.AddScoped<IUserRepository, UserRepository>();
            // todo services.AddScoped<IRunningActivityRepository, RunningActivityRepository>();

            return services;
        }
    }
}
