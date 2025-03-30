using FastEndpoints;
using MediatR;
using MovieService.UseCases.Projection.GetById;
using MovieService.UseCases.Projection.Update;

namespace MovieService.WebApi.Projection
{
    public class Update(IMediator mediator) : Endpoint<UpdateProjectionRequest, UpdateProjectionResponse>
    {
        private readonly IMediator _mediator = mediator;

        public override void Configure()
        {
            Put(UpdateProjectionRequest.Route);
            AllowAnonymous();
        }

        public override async Task HandleAsync(UpdateProjectionRequest request, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(
                new UpdateProjectionCommand(
                    request.ProjectionId,
                    request.StartTime,
                    request.MovieId,
                    request.HallId),

                cancellationToken
            );

            if (!result.IsSuccess || result.ErrorMessage == "Not Found")
            {
                await SendNotFoundAsync(cancellationToken);
                return;
            }

            var query = new GetProjectionByIdQuery(request.ProjectionId);
            var queryResult = await _mediator.Send(query, cancellationToken);

            if (!queryResult.IsSuccess || queryResult.ErrorMessage == "Not Found")
            {
                await SendNotFoundAsync(cancellationToken);
                return;
            }

            if (queryResult.IsSuccess)
            {
                var dto = queryResult.Value;
                Response = new UpdateProjectionResponse(
                    new ProjectionRecord(
                        dto.Id,
                        dto.StartTime,
                        dto.MovieId,
                        dto.HallId
                    )
                );
                return;
            }

        }
    }
}
