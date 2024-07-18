using Moq;
using RunningTracker.Application.RunningActivities.GetRunningActivities;
using RunningTracker.Domain.Activities;
using RunningTracker.Domain.Users;

namespace RunningTracker.Application.Tests.RunningActivities.GetRunningActivities
{
    public class GetRunningActivitiesQueryHandlerTests
    {
        [Fact]
        public async Task Handle_UserHasActivities_ReturnsActivities()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var activities = new List<RunningActivity>
            {
                new() {
                    Id = Guid.NewGuid(),
                    Location = "Park",
                    StartTime = DateTime.UtcNow,
                    EndTime = DateTime.UtcNow.AddHours(1),
                    Distance = 5.0,
                    User = new User
                    {
                        Id = userId,
                        Name = "John Centeno"
                    }
                }
            };

            var mockRunningActivityRepository = new Mock<IRunningActivityRepository>();
            mockRunningActivityRepository.Setup(repo => repo.GetUserRunningActivitiesAsync(userId))
                                         .ReturnsAsync(activities);

            var handler = new GetRunningActivitiesQueryHandler(mockRunningActivityRepository.Object);
            var query = new GetRunningActivitiesQuery(userId);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Single(result.Value);
            Assert.Equal(activities[0].Location, result.Value[0].Location);
            mockRunningActivityRepository.Verify(repo => repo.GetUserRunningActivitiesAsync(userId), Times.Once);
        }

        [Fact]
        public async Task Handle_UserHasNoActivities_ReturnsNotFoundError()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var activities = new List<RunningActivity>();

            var mockRunningActivityRepository = new Mock<IRunningActivityRepository>();
            mockRunningActivityRepository.Setup(repo => repo.GetUserRunningActivitiesAsync(userId))
                                         .ReturnsAsync(activities);

            var handler = new GetRunningActivitiesQueryHandler(mockRunningActivityRepository.Object);
            var query = new GetRunningActivitiesQuery(userId);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(RunninActivityErrors.NotFoundForUser(userId), result.Error);
            mockRunningActivityRepository.Verify(repo => repo.GetUserRunningActivitiesAsync(userId), Times.Once);
        }
    }
}
