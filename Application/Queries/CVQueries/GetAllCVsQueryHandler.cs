using Domain.Models;
using MediatR;

namespace Application.Queries.CVQueries
{
    public class GetAllCVsQueryHandler : IRequestHandler<GetAllCVsQuery, List<CV>>
    {
        public Task<List<CV>> Handle(GetAllCVsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
