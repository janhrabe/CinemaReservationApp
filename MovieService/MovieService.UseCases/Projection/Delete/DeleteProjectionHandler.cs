using Marten;
using MediatR;
using MovieService.Core;
using MovieService.Core.Entities;

namespace MovieService.UseCases.Projection.Delete
{
    public class DeleteProjectionHandler(IDocumentSession session)
     : IRequestHandler<DeleteProjectionCommand, Result>
    {
        public async Task<Result> Handle(DeleteProjectionCommand request, CancellationToken cancellationToken)
        {
            var projection = await session.LoadAsync<ProjectionEntity>(request.ProjectionId, cancellationToken);

            if (projection is null)
            {
                return Result.Failure($"Projection with Id {request.ProjectionId} not found.");
            }

            session.Delete(projection);
            await session.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }


}
