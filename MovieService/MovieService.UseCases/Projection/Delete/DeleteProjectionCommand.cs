using MediatR;
using MovieService.Core;

namespace MovieService.UseCases.Projection.Delete
{
    public record DeleteProjectionCommand(Guid ProjectionId) : IRequest<Result>;
}
