using Moq;
using RunningTracker.Application.Abstractions.Data;
using RunningTracker.Application.RunningActivities.AddRunningActivity;
using RunningTracker.Domain.Activities;
using RunningTracker.Domain.Users;

namespace RunningTracker.Application.Tests.RunningActivities.AddRunningActivity
{
    public class AddRunningActivityCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var location = "Park";
            var startTime = DateTime.UtcNow;
            var endTime = startTime.AddHours(1);
            var distance = 5.0;
            var command = new AddRunningActivityCommand(userId, location, startTime, endTime, distance);

            var user = new User
            {
                Id = userId,
                Name = "John Centeno"
            };

            var mockRunningActivityRepository = new Mock<IRunningActivityRepository>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUserRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(user);

            var handler = new AddRunningActivityCommandHandler(
                mockRunningActivityRepository.Object,
                mockUserRepository.Object,
                mockUnitOfWork.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            mockRunningActivityRepository.Verify(r => r.Add(It.IsAny<RunningActivity>()), Times.Once);
            mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_UserNotFound_ReturnsNotFoundError()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var location = "Park";
            var startTime = DateTime.UtcNow;
            var endTime = startTime.AddHours(1);
            var distance = 5.0;
            var command = new AddRunningActivityCommand(userId, location, startTime, endTime, distance);

            var mockRunningActivityRepository = new Mock<IRunningActivityRepository>();
            var mockUserRepository = new Mock<IUserRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            mockUserRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync((User)null);

            var handler = new AddRunningActivityCommandHandler(
                mockRunningActivityRepository.Object,
                mockUserRepository.Object,
                mockUnitOfWork.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(UserErrors.NotFound(userId), result.Error);
            mockRunningActivityRepository.Verify(r => r.Add(It.IsAny<RunningActivity>()), Times.Never);
            mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
