using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.UserCommands.UpdateUser
{
   
        public class UpdateUserByIdCommandHandler : IRequestHandler<UpdateUserByIdCommand, IdentityResult>
        {
            private readonly UserManager<IdentityUser> _userManager;
            private readonly ILogger<UpdateUserByIdCommandHandler> _logger;

            public UpdateUserByIdCommandHandler(UserManager<IdentityUser> userManager, ILogger<UpdateUserByIdCommandHandler> logger)
            {
                _userManager = userManager;
                _logger = logger;
            }

            public async Task<IdentityResult> Handle(UpdateUserByIdCommand request, CancellationToken cancellationToken)
            {
                _logger.LogInformation("Attempting to update user with ID: {UserId}", request.UserId);

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

                // Uppdatera användarnamn och e-post om de är angivna
                if (!string.IsNullOrEmpty(request.Username))
                {
                    user.UserName = request.Username;
                }

                if (!string.IsNullOrEmpty(request.Email))
                {
                    user.Email = request.Email;
                }

                // Försök att uppdatera användaren i databasen
                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    _logger.LogError("Failed to update user with ID: {UserId}. Errors: {Errors}",
                        request.UserId, string.Join(", ", updateResult.Errors));
                    return updateResult;
                }

                // Uppdatera lösenord 
                if (!string.IsNullOrEmpty(request.Password))
                {
                    var passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var passwordUpdateResult = await _userManager.ResetPasswordAsync(user, passwordResetToken, request.Password);

                    if (!passwordUpdateResult.Succeeded)
                    {
                        _logger.LogError("Failed to update password for user with ID: {UserId}. Errors: {Errors}",
                            request.UserId, string.Join(", ", passwordUpdateResult.Errors));
                        return passwordUpdateResult;
                    }
                }

                _logger.LogInformation("Successfully updated user with ID: {UserId}", request.UserId);

                return IdentityResult.Success;
            }
        }
    }


