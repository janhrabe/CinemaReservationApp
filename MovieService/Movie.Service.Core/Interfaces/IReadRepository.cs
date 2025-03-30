using System.Linq.Expressions;

namespace MovieService.Core.Interfaces
{
    public interface IReadRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        Task<List<T>> ListAsync(CancellationToken cancellationToken);
        Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken);

    }
}
