using RunningTracker.Domain.Activities;
using RunningTracker.Infrastructure.Shared;

namespace RunningTracker.Domain.Users
{
    public class User : Entity
    {
        public required string Name { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age => DateTime.Now.Year - BirthDate.Year;
        public double BMI => Weight / ((Height / 100) * (Height / 100));
        public ICollection<RunningActivity> RunningActivities { get; set; } = new List<RunningActivity>();
    }
}
