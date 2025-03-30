using FastEndpoints;
using MediatR;
using MovieService.UseCases.Movie.List;

namespace MovieService.WebApi.Movies
{
    public class ListMovies(IMediator _mediator) : EndpointWithoutRequest<MovieListResponse>
    {
        public override void Configure()
        {
            Get("/movies");
            AllowAnonymous();
        }

        public override async Task HandleAsync(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new ListMovieQuery(), cancellationToken);

            if (result.IsSuccess)
            {
                Response = new MovieListResponse
                {
                    Movies = result.Value.Select(m => new MovieRecord(m.Title, m.Description, m.Director, m.Cast, m.DurationInMinutes, m.isPlaying)).ToList()
                };
            }
        }
    }
}

