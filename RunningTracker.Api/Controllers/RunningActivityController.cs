using MediatR;
using Microsoft.AspNetCore.Mvc;
using RunningTracker.Api.Models;
using RunningTracker.Application.RunningActivities.AddRunningActivity;
using RunningTracker.Application.RunningActivities.GetRunningActivities;
using RunningTracker.Application.RunningActivities.GetRunningActivity;

namespace RunningTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RunningActivityController(ISender sender) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRunningActivity(Guid id, CancellationToken cancellationToken = default)
        {
            var query = new GetRunningActivityQuery(id);
            var result = await sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

        [HttpGet]
        public async Task<IActionResult> GetRunningActivities([FromQuery] Guid userId, CancellationToken cancellationToken = default)
        {
            var query = new GetRunningActivitiesQuery(userId);
            var result = await sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

        [HttpPost]
        public async Task<IActionResult> AddRunningActivity([FromQuery] Guid userId, RunningActivityRequest request, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new AddRunningActivityCommand(userId, request.Location, request.StartTime, request.EndTime, request.Distance);
            var result = await sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }
    }
}
