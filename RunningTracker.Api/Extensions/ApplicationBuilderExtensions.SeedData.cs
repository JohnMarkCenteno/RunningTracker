using RunngTracker.Persistence;
using RunningTracker.Domain.Users;

namespace RunningTracker.Api.Extensions
{
    public static partial class ApplicationBuilderExtensions
    {
        public static void UseSeedData(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                using (var serviceScope = app.ApplicationServices.CreateScope())
                {
                    var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                    SeedData(dbContext);
                }

                await next.Invoke();
            });
        }

        public static void SeedData(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.AddRange(
                new User
                {
                    Id = Guid.Parse("3fa85f64-5717-4562-b3fc-2c963f66afa6"),
                    Name = "John Centeno",
                    Weight = 60,
                    Height = 178,
                    BirthDate = new DateTime(1996, 4, 13),
                    RunningActivities = []
                });

                context.SaveChanges();
            }
        }
    }
}
