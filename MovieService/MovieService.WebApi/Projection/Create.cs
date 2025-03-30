using FastEndpoints;
using MediatR;
using MovieService.UseCases.Projection.Create;

namespace MovieService.WebApi.Projection
{
    public class CreateProjectionEndpoint : Endpoint<CreateProjectionRequest, CreateProjectionResponse>
    {
        private readonly IMediator _mediator;

        public CreateProjectionEndpoint(IMediator mediator)
        {
            _mediator = mediator;
        }

        public override void Configure()
        {
            Post("/projections");
            AllowAnonymous();
            Summary(s =>
            {
                s.ExampleRequest = new CreateProjectionRequest
                {
                    StartTime = DateTime.Now,
                    MovieId = Guid.NewGuid(),
                    HallId = Guid.NewGuid(),
                };
            });
        }

        public override async Task HandleAsync(
            CreateProjectionRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateProjectionCommand(
                request.StartTime,
                request.MovieId,
                request.HallId), cancellationToken);

            if (result.IsSuccess)
            {
                Response = new CreateProjectionResponse(result.Value);
                return;
            }


            Response = new CreateProjectionResponse("Failed to create projection.");
        }
    }
}
