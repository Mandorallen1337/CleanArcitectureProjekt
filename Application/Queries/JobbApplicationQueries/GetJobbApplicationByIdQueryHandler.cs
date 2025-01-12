using Domain.Models;
using MediatR;

namespace Application.Queries.JobbApplicationQueries
{
    public class GetJobbApplicationByIdQueryHandler : IRequestHandler<GetJobbApplicationByIdQuery, JobbApplication>
    {
        public Task<JobbApplication> Handle(GetJobbApplicationByIdQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
