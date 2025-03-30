using FastEndpoints;
using MediatR;
using MovieService.UseCases.Movie.Detail;

namespace MovieService.WebApi.Movies
{
    public class Detail(IMediator _mediator) : EndpointWithoutRequest<DetailMovieResponse>
    {
        public override void Configure()
        {
            Get("/movies/Detail");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DetailListMovieQuery(), cancellationToken);

            if (result.IsSuccess)
            {
                Response = new DetailMovieResponse
                {
                    Movies = result.Value.Select(m => new DetailMovieRecord(m.Id, m.Title, m.Description, m.Director, m.Cast, m.DurationInMinutes, m.isPlaying)).ToList()
                };
            }
        }
    }
}

