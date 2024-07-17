using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RunningTracker.Domain.Activities;

namespace RunningTracker.Persistence.Configurations
{
    public class RunningActivityConfiguration : IEntityTypeConfiguration<RunningActivity>
    {
        public void Configure(EntityTypeBuilder<RunningActivity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.HasOne(m => m.User)
                   .WithMany(u => u.RunningActivities)
                   .HasForeignKey(m => m.UserId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
