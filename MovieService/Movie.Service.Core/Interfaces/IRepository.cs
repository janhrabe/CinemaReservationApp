namespace MovieService.Core.Interfaces
{
    public interface IRepository<T> : IReadRepository<T> where T : class
    {
        Task AddAsync(T entity, CancellationToken cancellationToken);
        Task UpdateAsync(T entity, CancellationToken cancellationToken);
        Task DeleteAsync(T entity, CancellationToken cancellationToken);

    }
}
