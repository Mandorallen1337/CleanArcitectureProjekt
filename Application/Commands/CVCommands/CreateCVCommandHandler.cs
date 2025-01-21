using Application.Interfaces.RepoInterface;
using Microsoft.Extensions.Logging;
using Domain.Models;
using MediatR;
using Application.Interfaces.BlobStorageInterface;
using Application.Interfaces.ValidateFileInterface;

namespace Application.Commands.CVCommands
{
    public class CreateCVCommandHandler : IRequestHandler<CreateCVCommand, CV>
    {
        private readonly IRepository<CV> _cvRepository;
        private readonly IBlobStorage _blobStorage;
        private readonly IValidateFile _validateFile;
        private readonly ILogger<CreateCVCommandHandler> _logger;

        public CreateCVCommandHandler(IRepository<CV> cvRepository, IBlobStorage blobStorage, IValidateFile validateFile, ILogger<CreateCVCommandHandler> logger)
        {
            _cvRepository = cvRepository;
            _blobStorage = blobStorage;
            _validateFile = validateFile;
            _logger = logger;
        }

        public async Task<CV> Handle(CreateCVCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating a new CV for User ID {UserId}.", request.UserId);

                bool isValid = await _validateFile.IsValidPDFAsync(request.NewCV);
                if (!isValid)
                {
                    _logger.LogWarning("Invalid CV fileformat uploaded by User ID {UserId}.", request.UserId);
                    throw new InvalidOperationException("Invalid file. Please upload a valid PDF.");
                }

                string fileUrl = await _blobStorage.UploadFileAsync(request.NewCV);

                var cvEntity = new CV
                {
                    FileUrl = fileUrl,
                    UploadDate = DateTime.UtcNow,
                    UserId = request.UserId
                };

                var createdEntity = await _cvRepository.CreateAsync(cvEntity);

                _logger.LogInformation("Successfully created a new CV with ID {CVId}.", createdEntity.Id);

                return createdEntity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new CV for User ID {UserId}.", request.UserId);
                throw;
            }
        }
    }
}
