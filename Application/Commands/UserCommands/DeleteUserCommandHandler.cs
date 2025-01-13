using Application.Interfaces.RepoInterface;
using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands.UserCommands
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, IdentityResult>
    {
        private readonly UserManager<User> _userManager;

        public DeleteUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<IdentityResult> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            if (request.User == null)
            {
                throw new ArgumentNullException(nameof(request.User), "User data must not be null.");
            }

            try
            {
                var result = await _userManager.DeleteAsync(request.User);
                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while deleting the user.", ex);
            }
        }
    }
}
