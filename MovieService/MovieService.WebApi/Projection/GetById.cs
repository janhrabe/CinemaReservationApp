using FastEndpoints;
using MediatR;
using MovieService.UseCases.Projection.GetById;

namespace MovieService.WebApi.Projection
{
    public class GetProjectionById(IMediator mediator) : Endpoint<GetProjectionByIdRequest, ProjectionRecord>
    {
        private readonly IMediator _mediator = mediator;

        public override void Configure()
        {
            Get(GetProjectionByIdRequest.Route);
            AllowAnonymous();
            Summary(s =>
            {
                s.ExampleRequest = new GetProjectionByIdRequest { ProjectionId = Guid.NewGuid() };
            });
        }

        public override async Task HandleAsync(GetProjectionByIdRequest request, CancellationToken cancellationToken)
        {
            var query = new GetProjectionByIdQuery(request.ProjectionId);

            var result = await _mediator.Send(query, cancellationToken);

            if (!result.IsSuccess || result.Value == null)
            {
                await SendNotFoundAsync(cancellationToken);
            }

            Response = new ProjectionRecord(
                result.Value.Id,
                result.Value.StartTime,
                result.Value.MovieId,
                result.Value.HallId

            );
        }
    }
}
