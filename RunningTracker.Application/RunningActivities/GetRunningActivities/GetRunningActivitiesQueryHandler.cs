using RunningTracker.Application.Abstractions.Messaging;
using RunningTracker.Domain.Activities;
using RunningTracker.Infrastructure.Shared;

namespace RunningTracker.Application.RunningActivities.GetRunningActivities
{
    internal sealed class GetRunningActivitiesQueryHandler(
        IRunningActivityRepository runningActivityRepository) : IQueryHandler<GetRunningActivitiesQuery, List<RunningActivityResponse>>
    {
        private readonly IRunningActivityRepository runningActivityRepository = runningActivityRepository;

        public async Task<Result<List<RunningActivityResponse>>> Handle(GetRunningActivitiesQuery request, CancellationToken cancellationToken)
        {
            var activities = await runningActivityRepository.GetUserRunningActivitiesAsync(request.UserId);

            if (!activities.Any())
            {
                return Result.Failure<List<RunningActivityResponse>>(RunninActivityErrors.NotFoundForUser(request.UserId));
            }

            var result = activities.Select(a => new RunningActivityResponse(
                 a.Id,
                 a.Location,
                 a.StartTime,
                 a.EndTime,
                 a.Distance,
                 a.Duration,
                 a.AveragePace));

            return result.ToList();
        }
    }
}
