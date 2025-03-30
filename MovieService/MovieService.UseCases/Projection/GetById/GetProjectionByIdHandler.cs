using Marten;
using MediatR;
using MovieService.Core;
using MovieService.Core.Entities;

namespace MovieService.UseCases.Projection.GetById
{
    public class GetProjectionByIdHandler(IDocumentSession session)
        : IRequestHandler<GetProjectionByIdQuery, Result<ProjectionByIdDTO>>
    {
        public async Task<Result<ProjectionByIdDTO>> Handle(GetProjectionByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await session.LoadAsync<ProjectionEntity>(request.ProjectionId, cancellationToken);
            if (entity == null) return Result<ProjectionByIdDTO>.NotFound();

            var dto = new ProjectionByIdDTO(entity.Id, entity.StartTime, entity.MovieId, entity.HallId);
            return Result<ProjectionByIdDTO>.Success(dto);
        }
    }

}
