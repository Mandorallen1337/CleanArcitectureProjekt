using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.UserCommands.DeleteUser
{
    public class DeleteUserByIdCommandHandler : IRequestHandler<DeleteUserByIdCommand, IdentityResult>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<DeleteUserByIdCommandHandler> _logger;

        public DeleteUserByIdCommandHandler(UserManager<IdentityUser> userManager, ILogger<DeleteUserByIdCommandHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IdentityResult> Handle(DeleteUserByIdCommand request, CancellationToken cancellationToken)
        {
            // Loggar försöket att ta bort en användare
            _logger.LogInformation("Attempting to delete user with ID: {UserId}", request.UserId);

            // Hämta användaren baserat på ID
            var user = await _userManager.FindByIdAsync(request.UserId);
            if (user == null)
            {
                _logger.LogError("User not found with ID: {UserId}", request.UserId);
                return IdentityResult.Failed(new IdentityError
                {
                    Code = "UserNotFound",
                    Description = $"No user found with ID {request.UserId}."
                });
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                _logger.LogInformation("User deleted successfully: {UserId}", request.UserId);
            }
            else
            {
                _logger.LogError("Failed to delete user: {UserId}. Errors: {Errors}",
                    request.UserId, string.Join(", ", result.Errors));
            }

            return result;
        }
    }
}

