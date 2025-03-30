using Marten;
using MovieService.Core.Entities;
using MovieService.Core.Interfaces;

namespace MovieService.Infrastructure.Repositories
{
    public class MovieRepository(IDocumentStore _store) : Repository<MovieEntity>(_store), IMovieRepository
    {
    }
}
