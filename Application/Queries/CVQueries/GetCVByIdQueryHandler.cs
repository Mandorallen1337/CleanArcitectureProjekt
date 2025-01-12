using Domain.Models;
using MediatR;

namespace Application.Queries.CVQueries
{
    public class GetCVByIdQueryHandler : IRequestHandler<GetCVByIdQuery, CV>
    {
        public Task<CV> Handle(GetCVByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
