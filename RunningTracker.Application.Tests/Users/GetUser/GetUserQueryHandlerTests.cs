using Moq;
using RunningTracker.Application.Users.GetUser;
using RunningTracker.Domain.Users;

namespace RunningTracker.Application.Tests.Users.GetUser
{
    public class GetUserQueryHandlerTests
    {
        [Fact]
        public async Task Handle_UserExists_ReturnsUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User
            {
                Id = userId,
                Name = "John Centeno",
                Weight = 70.0,
                Height = 175.0,
                BirthDate = new DateTime(1990, 1, 1)
            };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.GetByIdAsync(userId))
                              .ReturnsAsync(user);

            var handler = new GetUserQueryHandler(mockUserRepository.Object);
            var query = new GetUserQuery(userId);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(userId, result.Value.Id);
            mockUserRepository.Verify(repo => repo.GetByIdAsync(userId), Times.Once);
        }

        [Fact]
        public async Task Handle_UserDoesNotExist_ReturnsNotFoundError()
        {
            // Arrange
            var userId = Guid.NewGuid();

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.GetByIdAsync(userId))
                              .ReturnsAsync((User)null);

            var handler = new GetUserQueryHandler(mockUserRepository.Object);
            var query = new GetUserQuery(userId);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(UserErrors.NotFound(userId), result.Error);
            mockUserRepository.Verify(repo => repo.GetByIdAsync(userId), Times.Once);
        }
    }
}
