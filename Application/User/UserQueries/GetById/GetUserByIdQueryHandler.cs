using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.UserQueries.GetById
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, IdentityUser>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<GetUserByIdQueryHandler> _logger;

        public GetUserByIdQueryHandler(UserManager<IdentityUser> userManager, ILogger<GetUserByIdQueryHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IdentityUser> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching user with ID: {UserId}", request.UserId);

            var user = await _userManager.FindByIdAsync(request.UserId);

            if (user == null)
            {
                _logger.LogWarning("User with ID: {UserId} not found.", request.UserId);
                return null; 
            }

            _logger.LogInformation("User found: {Username}", user.UserName);
            return user;
        }
    }
}

