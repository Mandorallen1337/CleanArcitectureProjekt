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
            try
            {
                var cvToDelete = await _cvRepository.GetByIdAsync(request.Id, cancellationToken);
                if (cvToDelete == null)
                {
                    throw new KeyNotFoundException("CV not found with the provided ID.");
                }

                await _cvRepository.DeleteByIdAsync(request.Id);
                return cvToDelete;
            }
            catch (Exception ex)
            {
                throw new ApplicationException("An error occurred while deleting the CV.", ex);
            }
        }
    }
}
