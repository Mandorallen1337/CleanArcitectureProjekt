using Domain.Models;
using MediatR;

namespace Application.Queries.JobbApplicationQueries
{
    public class GetAllJobbApplicationsQuery : IRequest<List<JobbApplication>>
    {
    }
}
