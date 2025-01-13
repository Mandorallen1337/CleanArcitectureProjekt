using Application.Interfaces.RepoInterface;
using Domain.Models;
using MediatR;

namespace Application.Commands.CVCommands
{
    public class UpdateCVByIdCommandHandler : IRequestHandler<UpdateCVByIdCommand, CV>
    {
        private readonly IRepository<CV> _cvRepository;

        public UpdateCVByIdCommandHandler(IRepository<CV> cvRepository)
        {
            _cvRepository = cvRepository;
        }

        public async Task<CV> Handle(UpdateCVByIdCommand request, CancellationToken cancellationToken)
        {
            if (request.UpdatedCV == null)
            {
                throw new ArgumentNullException(nameof(request.UpdatedCV), "The updated CV data must not be null.");
            }

            try
            {
                var existingCV = await _cvRepository.GetByIdAsync(request.Id, cancellationToken);
                if (existingCV == null)
                {
                    throw new KeyNotFoundException("CV not found with the provided ID.");
                }

                request.UpdatedCV.Id = request.Id;
                await _cvRepository.UpdateAsync(request.UpdatedCV, cancellationToken);
                return request.UpdatedCV;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while updating the CV.", ex);
            }
        }
    }
}
