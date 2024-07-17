using RunningTracker.Application.Abstractions.Data;
using RunningTracker.Application.Abstractions.Messaging;
using RunningTracker.Domain.Users;
using RunningTracker.Infrastructure.Shared;

namespace RunningTracker.Application.Users.AddUser
{
    internal sealed class AddUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork) : ICommandHandler<AddUserCommand>
    {
        private readonly IUserRepository userRepository = userRepository;
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<Result> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User
            {
                Name = request.Name,
                Weight = request.Weight,
                Height = request.Height,
                BirthDate = request.BirthDate,
            };

            userRepository.Add(user);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(user);
        }
    }
}
