using Moq;
using RunningTracker.Application.RunningActivities.GetRunningActivity;
using RunningTracker.Domain.Activities;
using RunningTracker.Domain.Users;

namespace RunningTracker.Application.Tests.RunningActivities.GetRunningActivity
{
    public class GetRunningActivityQueryHandlerTests
    {
        [Fact]
        public async Task Handle_ActivityExists_ReturnsActivity()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var activityId = Guid.NewGuid();

            var activity = new RunningActivity
            {
                Id = activityId,
                Location = "Park",
                StartTime = DateTime.UtcNow,
                EndTime = DateTime.UtcNow.AddHours(1),
                Distance = 5.0,
                User = new User
                {
                    Id = userId,
                    Name = "John Centeno"
                }
            };

            var mockRunningActivityRepository = new Mock<IRunningActivityRepository>();
            mockRunningActivityRepository.Setup(repo => repo.GetByIdAsync(activityId))
                                         .ReturnsAsync(activity);

            var handler = new GetRunningActivityQueryHandler(mockRunningActivityRepository.Object);
            var query = new GetRunningActivityQuery(activityId);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(activityId, result.Value.Id);
            mockRunningActivityRepository.Verify(repo => repo.GetByIdAsync(activityId), Times.Once);
        }

        [Fact]
        public async Task Handle_ActivityDoesNotExist_ReturnsNotFoundError()
        {
            // Arrange
            var activityId = Guid.NewGuid();

            var mockRunningActivityRepository = new Mock<IRunningActivityRepository>();
            mockRunningActivityRepository.Setup(repo => repo.GetByIdAsync(activityId))
                                         .ReturnsAsync((RunningActivity)null);

            var handler = new GetRunningActivityQueryHandler(mockRunningActivityRepository.Object);
            var query = new GetRunningActivityQuery(activityId);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(RunninActivityErrors.NotFound(activityId), result.Error);
            mockRunningActivityRepository.Verify(repo => repo.GetByIdAsync(activityId), Times.Once);
        }
    }
}
