using Application.Interfaces.RepoInterface;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Queries.CVQueries
{
    public class GetAllCVsQueryHandler : IRequestHandler<GetAllCVsQuery, List<CV>>
    {
        private readonly IRepository<CV> _cvRepository;
        private readonly ILogger<GetAllCVsQueryHandler> _logger;

        public GetAllCVsQueryHandler(IRepository<CV> cvRepository, ILogger<GetAllCVsQueryHandler> logger)
        {
            _cvRepository = cvRepository;
            _logger = logger;
        }

        public async Task<List<CV>> Handle(GetAllCVsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var allCVs = await _cvRepository.GetAllAsync();

                if (allCVs.Count == 0)
                {
                    _logger.LogWarning("No CVs were found in the repository.");
                }

                _logger.LogInformation("Successfully fetched {Count} CVs.", allCVs.Count);

                return allCVs;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while fetching all CVs.");
                throw;
            }
        }
    }
}
