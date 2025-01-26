using Application.Interfaces.RepoInterface;
using Microsoft.Extensions.Logging;
using Domain.Models;
using MediatR;
using Application.Interfaces.BlobStorageInterface;

namespace Application.Commands.CVCommands
{
    public class UpdateCVByIdCommandHandler : IRequestHandler<UpdateCVByIdCommand, CV>
    {
        private readonly IRepository<CV> _cvRepository;
        private readonly IBlobStorage _blobStorage;
        private readonly ILogger<UpdateCVByIdCommandHandler> _logger;

        public UpdateCVByIdCommandHandler(IRepository<CV> cvRepository, IBlobStorage blobStorage, ILogger<UpdateCVByIdCommandHandler> logger)
        {
            _cvRepository = cvRepository;
            _blobStorage = blobStorage;
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

                if (!string.IsNullOrEmpty(existingEntity.FileUrl))
                {
                    var oldBlobName = Path.GetFileName(new Uri(existingEntity.FileUrl).LocalPath);
                    var blobDeleted = await _blobStorage.DeleteFileAsync(oldBlobName);

                    if (!blobDeleted)
                    {
                        _logger.LogWarning("Failed to delete old blob with name {BlobName}.", oldBlobName);
                    }
                }

                string newFileUrl = null!;
                if (request.UpdatedCV != null)
                {
                    newFileUrl = await _blobStorage.UploadFileAsync(request.UpdatedCV);
                    existingEntity.FileName = request.FileName;
                }

                existingEntity.FileUrl = newFileUrl ?? existingEntity.FileUrl;
                existingEntity.FileName = request.FileName ?? existingEntity.FileName;
                existingEntity.UploadDate = DateTime.UtcNow;
                existingEntity.UserId = Guid.Parse(request.UserId);

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