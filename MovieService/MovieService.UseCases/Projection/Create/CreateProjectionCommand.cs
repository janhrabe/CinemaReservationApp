using MediatR;
using MovieService.Core;

namespace MovieService.UseCases.Projection.Create
{
    public record CreateProjectionCommand(DateTime StartTime, Guid MovieId, Guid HallId) : IRequest<Result<Guid>>;
}
