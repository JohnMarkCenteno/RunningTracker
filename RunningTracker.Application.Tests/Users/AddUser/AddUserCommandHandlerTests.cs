using Moq;
using RunningTracker.Application.Abstractions.Data;
using RunningTracker.Application.Users.AddUser;
using RunningTracker.Domain.Users;

namespace RunningTracker.Application.Tests.Users.AddUser
{
    public class AddUserCommandHandlerTests
    {
        [Fact]
        public async Task Handle_ValidCommand_ReturnsSuccessResult()
        {
            // Arrange
            var userName = "John Centeno";
            var weight = 70.0;
            var height = 175.0;
            var birthDate = new DateTime(1990, 1, 1);
            var command = new AddUserCommand(userName, weight, height, birthDate);

            var mockUserRepository = new Mock<IUserRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();

            var handler = new AddUserCommandHandler(mockUserRepository.Object, mockUnitOfWork.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.IsSuccess);
            mockUserRepository.Verify(r => r.Add(It.IsAny<User>()), Times.Once);
            mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task Handle_SaveChangesFails_ReturnsFailureResult()
        {
            // Arrange
            var userName = "John Centeno";
            var weight = 70.0;
            var height = 175.0;
            var birthDate = new DateTime(1990, 1, 1);
            var command = new AddUserCommand(userName, weight, height, birthDate);

            var mockUserRepository = new Mock<IUserRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.SaveChangesAsync(It.IsAny<CancellationToken>())).ThrowsAsync(new Exception("Save failed"));

            var handler = new AddUserCommandHandler(mockUserRepository.Object, mockUnitOfWork.Object);

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.IsSuccess);
            mockUserRepository.Verify(r => r.Add(It.IsAny<User>()), Times.Once);
            mockUnitOfWork.Verify(u => u.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
