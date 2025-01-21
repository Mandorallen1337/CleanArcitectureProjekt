using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.UserQueries.Login
{
    public class LoginUserQueryHandler : IRequestHandler<LoginUserQuery, LoginUserResponse>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginUserQueryHandler> _logger;

        public LoginUserQueryHandler(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, ILogger<LoginUserQueryHandler> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        public async Task<LoginUserResponse> Handle(LoginUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                _logger.LogWarning("Login failed for email: {Email} - User not found.", request.Email);
                return new LoginUserResponse
                {
                    IsSuccessful = false,
                    ErrorMessage = "Invalid email or password."
                };
            }

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                _logger.LogWarning("Login failed for email: {Email} - Incorrect password.", request.Email);
                return new LoginUserResponse
                {
                    IsSuccessful = false,
                    ErrorMessage = "Invalid email or password."
                };
            }

            string token = "GENERATED_TOKEN"; // Replace with actual token logic

            _logger.LogInformation("User logged in successfully: {Email}", request.Email);

            return new LoginUserResponse
            {
                IsSuccessful = true,
                Token = token
            };
        }
    }
}
