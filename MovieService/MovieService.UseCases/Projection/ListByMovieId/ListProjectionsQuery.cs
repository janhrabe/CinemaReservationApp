using MediatR;
using MovieService.Core;

namespace MovieService.UseCases.Projection.ListByMovieId
{
    public record ListProjectionsQuery(Guid MovieId) : IRequest<Result<List<ListProjectionDTO>>>;
}
