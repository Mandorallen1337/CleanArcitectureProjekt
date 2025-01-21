using Application.Interfaces.RepoInterface;
using Microsoft.Extensions.Logging;
using Domain.Models;
using MediatR;

namespace Application.Commands.CVCommands
{
    public class CreateCVCommandHandler : IRequestHandler<CreateCVCommand, CV>
    {
        private readonly IRepository<CV> _cvRepository;
        private readonly ILogger<CreateCVCommandHandler> _logger;

        public CreateCVCommandHandler(IRepository<CV> cvRepository, ILogger<CreateCVCommandHandler> logger)
        {
            _cvRepository = cvRepository;
            _logger = logger;
        }

        public async Task<CV> Handle(CreateCVCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Creating a new CV for User ID {UserId}.", request.NewCV.UserId);

                var cvEntity = new CV
                {
                    FileUrl = request.NewCV.FileUrl,
                    UploadDate = DateTime.UtcNow,
                    UserId = request.NewCV.UserId
                };

                var createdEntity = await _cvRepository.CreateAsync(cvEntity);

                _logger.LogInformation("Successfully created a new CV with ID {CVId}.", createdEntity.Id);

                return createdEntity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a new CV for User ID {UserId}.", request.NewCV.UserId);
                throw;
            }
        }
    }
}
