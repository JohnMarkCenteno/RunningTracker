using RunningTracker.Domain.Users;
using RunningTracker.Infrastructure.Shared;

namespace RunningTracker.Domain.Activities
{
    public class RunningActivity : Entity
    {
        public required string Location { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double Distance { get; set; }
        public double Duration => (EndTime - StartTime).TotalMinutes;
        public double AveragePace => Duration / Distance;
        public Guid UserId { get; set; }
        public required User User { get; set; }
    }
}
