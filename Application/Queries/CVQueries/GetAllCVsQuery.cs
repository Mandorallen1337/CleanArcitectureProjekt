using Domain.Models;
using MediatR;

namespace Application.Queries.CVQueries
{
    public class GetAllCVsQuery : IRequest<List<CV>>
    {
    }
}
