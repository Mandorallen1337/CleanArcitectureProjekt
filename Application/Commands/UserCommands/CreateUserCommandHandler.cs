using Application.Interfaces.RepoInterface;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands.UserCommands
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, IdentityResult>
    {
        private readonly UserManager<User> _userManager;

        public CreateUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.NewUser == null || string.IsNullOrWhiteSpace(request.Password))
            {
                throw new ArgumentNullException("User data or password must not be null.");
            }

            try
            {
                var result = await _userManager.CreateAsync(request.NewUser, request.Password);
                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while creating the user.", ex);
            }
        }
    }
}
