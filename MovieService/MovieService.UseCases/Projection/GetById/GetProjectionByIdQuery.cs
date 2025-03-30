using MediatR;
using MovieService.Core;

namespace MovieService.UseCases.Projection.GetById
{
    public record GetProjectionByIdQuery(Guid ProjectionId) : IRequest<Result<ProjectionByIdDTO>>;
}
