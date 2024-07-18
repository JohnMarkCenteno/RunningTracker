using Moq;
using RunningTracker.Application.Users.GetUsers;
using RunningTracker.Domain.Users;

namespace RunningTracker.Application.Tests.Users.GetUsers
{
    public class GetUsesQueryHandlerTests
    {
        [Fact]
        public async Task Handle_UsersExist_ReturnsUsers()
        {
            // Arrange
            var users = new List<User>
        {
            new User
            {
                Id = Guid.NewGuid(),
                Name = "John Centeno",
                Weight = 70.0,
                Height = 175.0,
                BirthDate = new DateTime(1990, 1, 1)
            },
            new User
            {
                Id = Guid.NewGuid(),
                Name = "Stella Centeno",
                Weight = 60.0,
                Height = 165.0,
                BirthDate = new DateTime(1992, 2, 2)
            }
        };

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.GetUsersAsync())
                              .ReturnsAsync(users);

            var handler = new GetUsersQueryHandler(mockUserRepository.Object);
            var query = new GetUsersQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Value);
            Assert.Equal(users.Count, result.Value.Count);
            mockUserRepository.Verify(repo => repo.GetUsersAsync(), Times.Once);
        }

        [Fact]
        public async Task Handle_NoUsersExist_ReturnsNotFoundError()
        {
            // Arrange
            var users = new List<User>();

            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(repo => repo.GetUsersAsync())
                              .ReturnsAsync(users);

            var handler = new GetUsersQueryHandler(mockUserRepository.Object);
            var query = new GetUsersQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            Assert.Equal(UserErrors.NotFound(), result.Error);
            mockUserRepository.Verify(repo => repo.GetUsersAsync(), Times.Once);
        }
    }
}
