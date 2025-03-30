using Marten;
using MovieService.Core.Entities;
using MovieService.Core.Interfaces;

namespace MovieService.Infrastructure.Repositories
{
    public class ProjectionRepository(IDocumentStore _store) : Repository<ProjectionEntity>(_store), IProjectionRepository
    {
    }
}
