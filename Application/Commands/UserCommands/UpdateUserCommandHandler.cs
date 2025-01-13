using Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands.UserCommands
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, IdentityResult>
    {
        private readonly UserManager<User> _userManager;

        public UpdateUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IdentityResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (request.UpdatedUser == null)
            {
                throw new ArgumentNullException(nameof(request.UpdatedUser), "Updated user data must not be null.");
            }

            try
            {
                var result = await _userManager.UpdateAsync(request.UpdatedUser);
                return result;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the user.", ex);
            }
        }
    }
}
