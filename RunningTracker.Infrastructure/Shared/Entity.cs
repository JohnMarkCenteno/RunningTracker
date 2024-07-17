using System.ComponentModel.DataAnnotations;

namespace RunningTracker.Infrastructure.Shared
{
    public abstract class Entity
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
