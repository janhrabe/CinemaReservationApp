using MediatR;
using MovieService.Core;

namespace MovieService.UseCases.Projection.Update
{
    public record UpdateProjectionCommand(Guid ProjectionId, DateTime StartTime, Guid MovieId, Guid HallId) : IRequest<Result<ProjectionDTO>>;
}
