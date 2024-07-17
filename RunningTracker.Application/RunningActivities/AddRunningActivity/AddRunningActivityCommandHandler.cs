using RunningTracker.Application.Abstractions.Data;
using RunningTracker.Application.Abstractions.Messaging;
using RunningTracker.Domain.Activities;
using RunningTracker.Domain.Users;
using RunningTracker.Infrastructure.Shared;

namespace RunningTracker.Application.RunningActivities.AddRunningActivity
{
    internal sealed class AddRunningActivityCommandHandler(
        IRunningActivityRepository runningActivityRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork) : ICommandHandler<AddRunningActivityCommand>
    {
        private readonly IRunningActivityRepository runningActivityRepository = runningActivityRepository;
        private readonly IUserRepository userRepository = userRepository;
        private readonly IUnitOfWork unitOfWork = unitOfWork;

        public async Task<Result> Handle(AddRunningActivityCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.UserId);

            if (user is null)
            {
                return UserErrors.NotFound(request.UserId);
            }

            var activity = new RunningActivity
            {
                Location = request.Location,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                Distance = request.Distance,
                User = user,
            };

            runningActivityRepository.Add(activity);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}
