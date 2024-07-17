using RunningTracker.Application.Abstractions.Messaging;
using RunningTracker.Domain.Users;
using RunningTracker.Infrastructure.Shared;

namespace RunningTracker.Application.Users.GetUser
{
    internal sealed class GetUserQueryHandler(
        IUserRepository userRepository) : IQueryHandler<GetUserQuery, UserResponse>
    {
        private readonly IUserRepository userRepository = userRepository;

        public async Task<Result<UserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.Id);

            if (user is null)
            {
                return Result.Failure<UserResponse>(UserErrors.NotFound(request.Id));
            }

            var result = new UserResponse(
                user.Id,
                user.Name,
                user.Weight,
                user.Height,
                user.BirthDate,
                user.Age,
                user.BMI);

            return result;
        }
    }
}
