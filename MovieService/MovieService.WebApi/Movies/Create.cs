using FastEndpoints;
using MediatR;
using MovieService.UseCases.Movie.Create;

namespace MovieService.WebApi.Movies
{
    public class CreateMovieEndpoint(IMediator mediator) : Endpoint<CreateMovieRequest, CreateMovieResponse>
    {
        private readonly IMediator _mediator = mediator;

        public override void Configure()
        {
            Post("/movies");
            AllowAnonymous();
            Summary(s =>
            {
                s.ExampleRequest = new CreateMovieRequest
                {
                    Title = "Movie Title",
                    Description = "Movie Description",
                    Director = "Director Name",
                    Cast = "Actor 1, Actor 2",
                    DurationInMinutes = 120,
                    IsPlaying = true,
                };
            });
        }

        public override async Task HandleAsync(
            CreateMovieRequest request,
            CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new CreateMovieCommand(
                request.Title,
                request.Description,
                request.Director,
                request.Cast,
                request.DurationInMinutes,
                request.IsPlaying), cancellationToken);

            if (result.IsSuccess)
            {
                Response = new CreateMovieResponse(result.Value);
                return;
            }


            Response = new CreateMovieResponse("Failed to create movie.");
        }
    }


}
