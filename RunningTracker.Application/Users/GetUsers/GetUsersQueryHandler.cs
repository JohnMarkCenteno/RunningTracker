using RunningTracker.Application.Abstractions.Messaging;
using RunningTracker.Domain.Users;
using RunningTracker.Infrastructure.Shared;

namespace RunningTracker.Application.Users.GetUsers
{
    internal class GetUsersQueryHandler(
        IUserRepository userRepository) : IQueryHandler<GetUsersQuery, List<UserResponse>>
    {
        private readonly IUserRepository userRepository = userRepository;

        public async Task<Result<List<UserResponse>>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await userRepository.GetUsersAsync();

            if (!users.Any())
            {
                return Result.Failure<List<UserResponse>>(UserErrors.NotFound());
            }

            var result = users.Select(user => new UserResponse(
                 user.Id,
                 user.Name,
                 user.Weight,
                 user.Height,
                 user.BirthDate,
                 user.Age,
                 user.BMI));

            return result.ToList();
        }
    }
}
