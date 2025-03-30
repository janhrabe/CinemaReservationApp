using Marten;
using MediatR;
using MovieService.Core;
using MovieService.Core.Entities;

namespace MovieService.UseCases.Projection.Update
{
    public class UpdateProjectionHandler(IDocumentSession session)
    : IRequestHandler<UpdateProjectionCommand, Result<ProjectionDTO>>
    {
        public async Task<Result<ProjectionDTO>> Handle(UpdateProjectionCommand request, CancellationToken cancellationToken)
        {
            var existingProjection = await session.LoadAsync<ProjectionEntity>(request.ProjectionId, cancellationToken);
            if (existingProjection == null)
                return Result<ProjectionDTO>.NotFound();

            existingProjection.StartTime = request.StartTime;
            existingProjection.MovieId = request.MovieId;
            existingProjection.HallId = request.HallId;

            await session.SaveChangesAsync(cancellationToken);

            var updatedProjectionDTO = new ProjectionDTO(existingProjection.StartTime, existingProjection.MovieId, existingProjection.HallId);

            return Result<ProjectionDTO>.Success(updatedProjectionDTO);
        }
    }


}
