using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.UserQueries.GetAll
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUserQuery, List<IdentityUser>>
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ILogger<GetAllUserQueryHandler> _logger;

        public GetAllUserQueryHandler(UserManager<IdentityUser> userManager, ILogger<GetAllUserQueryHandler> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<List<IdentityUser>> Handle(GetAllUserQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fetching all users.");

            // Hämtar alla användare direkt som IdentityUser
            var users = await Task.FromResult(_userManager.Users.ToList());

            _logger.LogInformation("Fetched {UserCount} users.", users.Count);

            return users;
        }
    }
}

