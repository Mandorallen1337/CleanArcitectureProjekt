using Application.Interfaces.RepoInterface;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Queries.CVQueries
{
    public class GetCVByIdQueryHandler : IRequestHandler<GetCVByIdQuery, CV>
    {
        private readonly IRepository<CV> _cvRepository;
        private readonly ILogger<GetCVByIdQueryHandler> _logger;

        public GetCVByIdQueryHandler(IRepository<CV> cvRepository, ILogger<GetCVByIdQueryHandler> logger)
        {
            _cvRepository = cvRepository;
            _logger = logger;
        }

        public async Task<CV> Handle(GetCVByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Fetching CV with ID {CVId}.", request.Id);

                var cv = await _cvRepository.GetByIdAsync(request.Id, cancellationToken);

                if (cv == null)
                {
                    _logger.LogWarning("CV with ID {CVId} was not found.", request.Id);
                    throw new KeyNotFoundException($"CV with ID {request.Id} was not found.");
                }

                _logger.LogInformation("Successfully retrieved CV with ID {CVId}.", request.Id);

                return cv;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching the CV with ID {CVId}.", request.Id);
                throw;
            }
        }
    }
}
