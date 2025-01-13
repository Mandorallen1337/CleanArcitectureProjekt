using MediatR;
using Domain.Models;
using Application.Interfaces.RepoInterface;

namespace Application.Commands.CVCommands
{
    public class CreateCVCommandHandler : IRequestHandler<CreateCVCommand, CV>
    {
        private readonly IRepository<CV> _cvRepository;

        public CreateCVCommandHandler(IRepository<CV> cvRepository)
        {
            _cvRepository = cvRepository;
        }

        public async Task<CV> Handle(CreateCVCommand request, CancellationToken cancellationToken)
        {
            if (request.NewCV == null)
            {
                throw new ArgumentNullException(nameof(request.NewCV), "The CV data must not be null.");
            }

            try
            {
                var addedCV = await _cvRepository.CreateAsync(request.NewCV);
                return addedCV;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while adding the CV.", ex);
            }
        }
    }
}
