using Domain.Models;
using MediatR;

namespace Application.Queries.UserQueries
{
    public class GetUserByIdQuery : IRequest<User>
    {
        public GetUserByIdQuery(Guid id)
        {
            Id = id;
        }
        public Guid Id { get; }
    }
}
