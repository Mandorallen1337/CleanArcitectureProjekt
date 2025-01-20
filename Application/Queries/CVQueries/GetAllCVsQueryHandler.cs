using Application.Interfaces.RepoInterface;
using Domain.Models;
using MediatR;

namespace Application.Queries.CVQueries
{
    public class GetAllCVsQueryHandler : IRequestHandler<GetAllCVsQuery, List<CV>>
    {
        private readonly IRepository<CV> _cvRepository;

        public GetAllCVsQueryHandler(IRepository<CV> cvRepository)
        {
            _cvRepository = cvRepository;
        }

        public async Task<List<CV>> Handle(GetAllCVsQuery request, CancellationToken cancellationToken)
        {
            var allCVs = await _cvRepository.GetAllAsync();

            return allCVs;
        }
    }
}
