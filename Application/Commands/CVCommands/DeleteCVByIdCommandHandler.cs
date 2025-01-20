using Application.Interfaces.RepoInterface;
using Domain.Models;
using MediatR;

namespace Application.Commands.CVCommands
{
    public class DeleteCVByIdCommandHandler : IRequestHandler<DeleteCVByIdCommand, CV>
    {
        private readonly IRepository<CV> _cvRepository;

        public DeleteCVByIdCommandHandler(IRepository<CV> cvRepository)
        {
            _cvRepository = cvRepository;
        }
        public async Task<CV> Handle(DeleteCVByIdCommand request, CancellationToken cancellationToken)
        {
            var cvEntity = await _cvRepository.GetByIdAsync(request.Id, cancellationToken);

            if (cvEntity == null)
            {
                throw new KeyNotFoundException($"CV with ID {request.Id} was not found.");
            }

            await _cvRepository.DeleteByIdAsync(request.Id);

            return cvEntity;
        }
    }
}
