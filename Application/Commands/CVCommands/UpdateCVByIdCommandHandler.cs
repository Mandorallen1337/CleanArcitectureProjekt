using Application.Interfaces.RepoInterface;
using Microsoft.Extensions.Logging;
using Domain.Models;
using MediatR;

namespace Application.Commands.CVCommands
{
    public class UpdateCVByIdCommandHandler : IRequestHandler<UpdateCVByIdCommand, CV>
    {
        private readonly IRepository<CV> _cvRepository;
        private readonly ILogger<UpdateCVByIdCommandHandler> _logger;

        public UpdateCVByIdCommandHandler(IRepository<CV> cvRepository, ILogger<UpdateCVByIdCommandHandler> logger)
        {
            _cvRepository = cvRepository;
            _logger = logger;
        }

        public async Task<CV> Handle(UpdateCVByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Updating CV with ID {CVId}.", request.Id);

                var existingEntity = await _cvRepository.GetByIdAsync(request.Id, cancellationToken);

                if (existingEntity == null)
                {
                    _logger.LogWarning("CV with ID {CVId} was not found.", request.Id);
                    throw new KeyNotFoundException($"CV with ID {request.Id} was not found.");
                }

                existingEntity.FileUrl = request.UpdatedCV.FileUrl;
                existingEntity.UploadDate = DateTime.UtcNow;
                existingEntity.UserId = request.UpdatedCV.UserId;

                await _cvRepository.UpdateAsync(existingEntity, cancellationToken);

                _logger.LogInformation("Successfully updated CV with ID {CVId}.", request.Id);

                return existingEntity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating CV with ID {CVId}.", request.Id);
                throw;
            }
        }
    }
}