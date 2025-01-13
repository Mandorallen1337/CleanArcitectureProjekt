using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Queries.UserQueries
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<User>>
    {
        private readonly UserManager<User> _userManager;

        public GetAllUsersQueryHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<List<User>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return _userManager.Users.ToList();
        }
    }
}
