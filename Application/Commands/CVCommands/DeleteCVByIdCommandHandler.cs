using Application.Interfaces.RepoInterface;
using Microsoft.Extensions.Logging;
using Domain.Models;
using MediatR;

namespace Application.Commands.CVCommands
{
    public class DeleteCVByIdCommandHandler : IRequestHandler<DeleteCVByIdCommand, CV>
    {
        private readonly IRepository<CV> _cvRepository;
        private readonly ILogger<DeleteCVByIdCommandHandler> _logger;

        public DeleteCVByIdCommandHandler(IRepository<CV> cvRepository, ILogger<DeleteCVByIdCommandHandler> logger)
        {
            _cvRepository = cvRepository;
            _logger = logger;
        }
        public async Task<CV> Handle(DeleteCVByIdCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Deleting CV with ID {CVId}.", request.Id);

                var cvEntity = await _cvRepository.GetByIdAsync(request.Id, cancellationToken);

                if (cvEntity == null)
                {
                    _logger.LogWarning("CV with ID {CVId} was not found.", request.Id);
                    throw new KeyNotFoundException($"CV with ID {request.Id} was not found.");
                }

                await _cvRepository.DeleteByIdAsync(request.Id);

                _logger.LogInformation("Successfully deleted CV with ID {CVId}.", request.Id);

                return cvEntity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting CV with ID {CVId}.", request.Id);
                throw;
            }
        }
    }
}
