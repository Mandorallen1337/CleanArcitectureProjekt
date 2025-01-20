using Domain.Models;
using MediatR;

namespace Application.Queries.CVQueries
{
    public class GetCVByIdQuery : IRequest<CV>
    {
        public GetCVByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; set; }
    }
}
