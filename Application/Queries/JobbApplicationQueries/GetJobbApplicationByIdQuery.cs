using Domain.Models;
using MediatR;

namespace Application.Queries.JobbApplicationQueries
{
    public class GetJobbApplicationByIdQuery : IRequest<JobbApplication>
    {
        public GetJobbApplicationByIdQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }
    }
}
