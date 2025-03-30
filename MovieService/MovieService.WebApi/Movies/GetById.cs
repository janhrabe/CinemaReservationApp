using FastEndpoints;
using MediatR;
using MovieService.UseCases.Movie.GetById;

namespace MovieService.WebApi.Movies
{
    public class GetMovieById(IMediator mediator) : Endpoint<GetMovieByIdRequest, MovieRecord>
    {
        private readonly IMediator _mediator = mediator;

        public override void Configure()
        {
            Get("/movies/{MovieId:guid}");
            AllowAnonymous();

            Summary(s =>
            {
                s.ExampleRequest = new GetMovieByIdRequest { MovieId = Guid.NewGuid() };

            });
        }

        public override async Task HandleAsync(GetMovieByIdRequest request, CancellationToken cancellationToken)
        {
            var query = new GetMovieByIdQuery(request.MovieId);

            var result = await _mediator.Send(query, cancellationToken);

            if (!result.IsSuccess || result.Value == null)
            {
                await SendNotFoundAsync(cancellationToken);
            }

            Response = new MovieRecord(
                result.Value.Title,
                result.Value.Description,
                result.Value.Director,
                result.Value.Cast,
                result.Value.DurationInMinutes,
                result.Value.isPlaying
            );
        }
    }

}