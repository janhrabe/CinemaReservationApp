using System.Linq.Expressions;
using Marten;
using MovieService.Core.Interfaces;

namespace MovieService.Infrastructure.Repositories
{
    public class Repository<T>(IDocumentStore store) : IRepository<T> where T : class
    {
        private readonly IDocumentStore _store = store;

        public async Task AddAsync(T entity, CancellationToken cancellationToken)
        {
            using var session = _store.LightweightSession();
            session.Store(entity);
            await session.SaveChangesAsync(cancellationToken);
        }


        public async Task DeleteAsync(T entity, CancellationToken cancellationToken)
        {
            using var session = _store.LightweightSession();
            session.Delete(entity);
            await session.SaveChangesAsync(cancellationToken);
        }

        public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            using var session = _store.LightweightSession();
            return await session.LoadAsync<T>(id, cancellationToken);
        }


        public async Task<List<T>> ListAsync(CancellationToken cancellationToken)
        {
            using var session = _store.LightweightSession();
            var result = await session.Query<T>().ToListAsync(cancellationToken);
            return [.. result];
        }

        public async Task<List<T>> ListAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken)
        {
            using var session = _store.LightweightSession();
            var result = await session.Query<T>().Where(predicate).ToListAsync(cancellationToken);
            return result.ToList();
        }



        public async Task UpdateAsync(T entity, CancellationToken cancellationToken)
        {
            using var session = _store.LightweightSession();
            session.Store(entity);
            await session.SaveChangesAsync(cancellationToken);
        }
    }
}
