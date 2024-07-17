using RunningTracker.Application.Abstractions.Messaging;
using RunningTracker.Domain.Activities;
using RunningTracker.Infrastructure.Shared;

namespace RunningTracker.Application.RunningActivities.GetRunningActivity
{
    internal class GetRunningActivityQueryHandler(
        IRunningActivityRepository runningActivityRepository) : IQueryHandler<GetRunningActivityQuery, RunningActivityResponse>
    {
        private readonly IRunningActivityRepository runningActivityRepository = runningActivityRepository;

        public async Task<Result<RunningActivityResponse>> Handle(GetRunningActivityQuery request, CancellationToken cancellationToken)
        {
            var activity = await runningActivityRepository.GetByIdAsync(request.Id);

            if (activity is null)
            {
                return Result.Failure<RunningActivityResponse>(RunninActivityErrors.NotFound(request.Id));
            }

            var result = new RunningActivityResponse(
                 activity.Id,
                 activity.Location,
                 activity.StartTime,
                 activity.EndTime,
                 activity.Distance,
                 activity.Duration,
                 activity.AveragePace);

            return result;
        }
    }
}
