using Application.User.UserCommands.CreateUser;
using Application.User.UserCommands.DeleteUser;
using Application.User.UserCommands.UpdateUser;
using Application.User.UserQueries.GetAll;
using Application.User.UserQueries.GetById;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MyCvSite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UserController> _logger;

        public UserController(IMediator mediator, ILogger<UserController> logger)
        {
            _mediator = mediator;
            _logger = logger;
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllUsers()
        {
            _logger.LogInformation("Fetching all users.");
            var users = await _mediator.Send(new GetAllUserQuery());

            _logger.LogInformation("Fetched {UserCount} users.", users.Count);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            _logger.LogInformation("Fetching user with ID: {UserId}", id);

            var user = await _mediator.Send(new GetUserByIdQuery { UserId = id });

            if (user == null)
            {
                _logger.LogWarning("User with ID {UserId} not found.", id);
                return NotFound($"User with ID {id} not found.");
            }

            _logger.LogInformation("User found: {Username}", user.UserName);
            return Ok(user);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            _logger.LogInformation("Attempting to create a new user with username: {Username}", command.Username);

            var result = await _mediator.Send(command);

            if (result.Succeeded)
            {
                _logger.LogInformation("User created successfully: {Username}", command.Username);
                return Ok("User created successfully.");
            }

            _logger.LogWarning("Failed to create user: {Username}. Errors: {Errors}", command.Username, string.Join(", ", result.Errors.Select(e => e.Description)));
            return BadRequest(result.Errors);
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserByIdCommand command)
        {
            if (id != command.UserId)
            {
                _logger.LogWarning("User ID mismatch: URL ID {UrlId}, Command ID {CommandId}", id, command.UserId);
                return BadRequest("User ID in the URL does not match the User ID in the body.");
            }

            _logger.LogInformation("Attempting to update user with ID: {UserId}", id);
            var result = await _mediator.Send(command);

            if (result.Succeeded)
            {
                _logger.LogInformation("User updated successfully: {UserId}", id);
                return Ok("User updated successfully.");
            }

            _logger.LogError("Failed to update user with ID: {UserId}. Errors: {Errors}", id, string.Join(", ", result.Errors.Select(e => e.Description)));
            return BadRequest(result.Errors);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            _logger.LogInformation("Attempting to delete user with ID: {UserId}", id);

            var result = await _mediator.Send(new DeleteUserByIdCommand { UserId = id });

            if (result.Succeeded)
            {
                _logger.LogInformation("User deleted successfully: {UserId}", id);
                return Ok("User deleted successfully.");
            }

            _logger.LogError("Failed to delete user with ID: {UserId}. Errors: {Errors}", id, string.Join(", ", result.Errors.Select(e => e.Description)));
            return BadRequest(result.Errors);
        }
    }
}
