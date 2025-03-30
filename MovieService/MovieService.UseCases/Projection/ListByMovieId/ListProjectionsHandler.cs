using Marten;
using MediatR;
using MovieService.Core;
using MovieService.Core.Entities;

namespace MovieService.UseCases.Projection.ListByMovieId
{
    public class ListProjectionsHandler(IDocumentSession session)
     : IRequestHandler<ListProjectionsQuery, Result<List<ListProjectionDTO>>>
    {
        public async Task<Result<List<ListProjectionDTO>>> Handle(ListProjectionsQuery request, CancellationToken cancellationToken)
        {
            var projections = await session.Query<ProjectionEntity>()
                .Where(p => p.MovieId == request.MovieId)
                .ToListAsync(cancellationToken);

            if (projections == null || projections.Count == 0)
                return Result<List<ListProjectionDTO>>.NotFound();

            var projectionDTOs = projections
                .Select(p => new ListProjectionDTO(p.Id, p.MovieId, p.StartTime, p.HallId))
                .ToList();

            return Result<List<ListProjectionDTO>>.Success(projectionDTOs);
        }
    }

}
