using Domain.Models;
using MediatR;

namespace Application.Queries.JobbApplicationQueries
{
    public class GetAllJobbApplicationsQueryHandler : IRequestHandler<GetAllJobbApplicationsQuery, List<JobbApplication>>
    {
        public Task<List<JobbApplication>> Handle(GetAllJobbApplicationsQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
