using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.RepoInterface
{
    public interface IRepository<T>
    {
        Task<T> CreateAsync(T entity);

        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<List<T>> GetAllAsync();

        Task<string> DeleteByIdAsync(Guid id);

        Task UpdateAsync(T entity, CancellationToken cancellationToken);
    }
}
