using FastEndpoints;
using MovieService.Core.Interfaces;

namespace MovieService.WebApi.Projection
{


    public class DeleteProjectionEndpoint(IProjectionRepository repository) : Endpoint<DeleteProjectionRequest>
    {
        private readonly IProjectionRepository _repository = repository;

        public override void Configure()
        {

            Delete("/projections/{ProjectionId}");
            AllowAnonymous();
        }

        public override async Task HandleAsync(DeleteProjectionRequest request, CancellationToken cancellationToken)
        {

            var projection = await _repository.GetByIdAsync(request.ProjectionId, cancellationToken);

            if (projection == null)
            {
                await SendNotFoundAsync(cancellationToken);
                return;
            }

            await _repository.DeleteAsync(projection, cancellationToken);

            await SendOkAsync(cancellationToken);
        }

    }
}
