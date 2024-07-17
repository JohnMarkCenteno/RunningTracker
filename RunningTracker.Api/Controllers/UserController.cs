using MediatR;
using Microsoft.AspNetCore.Mvc;
using RunningTracker.Api.Models;
using RunningTracker.Application.RunningActivities.GetRunningActivities;
using RunningTracker.Application.Users.AddUser;
using RunningTracker.Application.Users.GetUser;
using RunningTracker.Application.Users.GetUsers;

namespace RunningTracker.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(ISender sender) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(Guid id, CancellationToken cancellationToken = default)
        {
            var query = new GetUserQuery(id);
            var result = await sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserRequest request, CancellationToken cancellationToken = default)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var command = new AddUserCommand(request.Name, request.Weight, request.Height, request.BirthDate);
            var result = await sender.Send(command, cancellationToken);

            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers(CancellationToken cancellationToken = default)
        {
            var query = new GetUsersQuery();
            var result = await sender.Send(query, cancellationToken);

            return result.IsSuccess ? Ok(result) : BadRequest(result.Error);
        }
    }
}
