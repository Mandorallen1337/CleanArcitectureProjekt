using Application.Interfaces.RepoInterface;
using Domain.Models;
using MediatR;

namespace Application.Queries.CVQueries
{
    public class GetCVByIdQueryHandler : IRequestHandler<GetCVByIdQuery, CV>
    {
        private readonly IRepository<CV> _cvRepository;

        public GetCVByIdQueryHandler(IRepository<CV> cvRepository)
        {
            _cvRepository = cvRepository;
        }

        public async Task<CV> Handle(GetCVByIdQuery request, CancellationToken cancellationToken)
        {
            var cv = await _cvRepository.GetByIdAsync(request.Id, cancellationToken);
            if (cv == null)
            {
                throw new KeyNotFoundException("CV not found with the provided ID.");
            }

            return cv;
        }
    }
}
